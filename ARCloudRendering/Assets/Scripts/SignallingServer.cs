using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using WebSocketSharp.Server;
using Unity.WebRTC;
using Newtonsoft.Json.Linq;
using HttpStatusCode = WebSocketSharp.Net.HttpStatusCode;

public class SignallingServer : MonoBehaviour
{
    public int port = 443;
    public Camera mainCamera, alphaCamera;
    private HttpServer httpServer;
    private RTCPeerConnection peerConnection;
    private RTCDataChannel receiveChannel;

    private RenderTexture cameraTexture, alphaTexture, encodedTexture;

    private VideoStreamTrack track;

    private Queue<string> signallingQueue = new Queue<string>();

    RTCConfiguration GetSelectedSdpSemantics()
    {
        RTCConfiguration config = default;
        config.iceServers = new RTCIceServer[]
        {
            new RTCIceServer { urls = new string[] { "stun:stun.l.google.com:19302" } }
        };

        return config;
    }

    public void Awake()
    {
        WebRTC.Initialize(EncoderType.Hardware);
    }

    public void Start()
    {
        var gfxType = SystemInfo.graphicsDeviceType;
        var format = WebRTC.GetSupportedRenderTextureFormat(gfxType);
        cameraTexture = new RenderTexture(Constants.WIDTH, Constants.HEIGHT, 0, format);
        alphaTexture = new RenderTexture(Constants.WIDTH, Constants.HEIGHT, 0, format);
        encodedTexture = new RenderTexture(Constants.WIDTH * 2, Constants.HEIGHT, 0, format);
        
        mainCamera.targetTexture = cameraTexture;
        alphaCamera.targetTexture = alphaTexture;
        track = new VideoStreamTrack("AR", encodedTexture);

        SetupSignallingServer();

        var configuration = GetSelectedSdpSemantics();
        peerConnection = new RTCPeerConnection(ref configuration);
        peerConnection.AddTrack(track);

        peerConnection.OnDataChannel += channel =>
        {
            receiveChannel = channel;
            Debug.Log("Received Data Channel");
            receiveChannel.OnMessage += ReceiveDataChannelMessage;
        };
        peerConnection.OnIceCandidate = iceCandidate =>
        {
            Candidate c = new Candidate{
                type = "candidate",
                candidate = new CandidateString {
                    candidate = iceCandidate.candidate,
                    sdpMid = iceCandidate.sdpMid,
                    sdpMLineIndex = iceCandidate.sdpMLineIndex,
                },
            };
            WebSocketGlobals.SendMessage(JsonUtility.ToJson(c));
        };
        peerConnection.OnIceConnectionChange = state => {
            Debug.Log($"ICE State: {state}");
        };
        StartCoroutine(WebRTC.Update());
    }

    public void SetupSignallingServer()
    {
        try
        {
            Debug.Log("Starting to create WebSocket Server");
            httpServer = new HttpServer(port);
            httpServer.DocumentRootPath = Path.Combine(Application.streamingAssetsPath, "webxr-client");

            // Set the HTTP GET request event.
            httpServer.OnGet += (sender, e) =>
            {
                var req = e.Request;
                var res = e.Response;
                var path = req.RawUrl;

                if (path == "/")
                    path += "index.html";

                byte[] contents;
                if (!e.TryReadFile(path, out contents))
                {
                    res.StatusCode = (int) HttpStatusCode.NotFound;
                    return;
                }

                if (path.EndsWith(".html"))
                {
                    res.ContentType = "text/html";
                    res.ContentEncoding = System.Text.Encoding.UTF8;
                }
                else if (path.EndsWith(".js"))
                {
                    res.ContentType = "application/javascript";
                    res.ContentEncoding = System.Text.Encoding.UTF8;
                }

                res.ContentLength64 = contents.LongLength;
                res.Close(contents, true);
            };

            Action<SignallingService> serviceInitializer = delegate(SignallingService service)
            {
                WebSocketGlobals.MessageReceived = body => signallingQueue.Enqueue(body);
                WebSocketGlobals.SendMessage = service.SendData;
            };
            httpServer.AddWebSocketService("/signalling", serviceInitializer);

            // httpServer.SslConfiguration.ServerCertificate =
            //     new X509Certificate2(Path.Combine(Application.streamingAssetsPath, "ssl", "server.pfx"), "mecal");
            // httpServer.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
            // httpServer.SslConfiguration.CheckCertificateRevocation = false;

            httpServer.Start();
            Debug.Log("Finished creating WebSocket Server");
        }
        catch (Exception e)
        {
            Debug.Log($"Error Creating WebSocket Server: {e.Message}");
        }
    }

    public void Update()
    {
        Graphics.CopyTexture(cameraTexture,  0, 0, 0, 0, cameraTexture.width, cameraTexture.height,
                             encodedTexture, 0, 0, 0, 0);
        Graphics.CopyTexture(alphaTexture,   0, 0, 0, 0, alphaTexture.width, alphaTexture.height,
                             encodedTexture, 0, 0, encodedTexture.width/2, 0);

        if (signallingQueue.Count > 0)
            ReceiveMessage(signallingQueue.Dequeue());
    }

    public void ReceiveMessage(string data)
    {
        Type json = JsonUtility.FromJson<Type>(data);
        Debug.Log($"Received message type: {json.type}");
        switch (json.type)
        {
            case "offer":
                SDP offer = JsonUtility.FromJson<SDP>(data);
                StartCoroutine(ReceiveOffer(offer.sdp));
                break;
            case "candidate":
                Candidate candidate = JsonUtility.FromJson<Candidate>(data);
                RTCIceCandidate iceCandidate = new RTCIceCandidate {
                    candidate = candidate.candidate.candidate,
                    sdpMid = candidate.candidate.sdpMid,
                    sdpMLineIndex = candidate.candidate.sdpMLineIndex,
                };
                peerConnection.AddIceCandidate(ref iceCandidate);
                break;
            case "leave":
                var configuration = GetSelectedSdpSemantics();
                peerConnection = new RTCPeerConnection(ref configuration);
                break;
            default:
                Debug.LogError($"Invalid Message Type: {data}");
                break;
        }
    }

    private IEnumerator ReceiveOffer(string offer)
    {
        RTCSessionDescription desc = new RTCSessionDescription() { sdp = offer, type = RTCSdpType.Offer };
        var op1 = peerConnection.SetRemoteDescription(ref desc);
        yield return op1;
        RTCAnswerOptions answerOptions = default;
        var op2 = peerConnection.CreateAnswer(ref answerOptions);
        yield return op2;
        RTCSessionDescription answerDesc = op2.Desc;
        var op3 = peerConnection.SetLocalDescription(ref answerDesc);
        yield return op3;
        SDP answer = new SDP{
            type = "answer",
            sdp = answerDesc.sdp,
        };
        WebSocketGlobals.SendMessage(JsonUtility.ToJson(answer));
    }

    public void ReceiveDataChannelMessage(byte[] bytes) 
    {
        Pose pose = JsonUtility.FromJson<Pose>(System.Text.Encoding.ASCII.GetString(bytes));
        //Calculate Quaternion from euler angles
        Vector3 position = new Vector3(pose.position.x, pose.position.y, -pose.position.z);
        Quaternion rotation =
            Quaternion.Inverse(new Quaternion(pose.rotation.x, pose.rotation.y, pose.rotation.z,
                pose.rotation.w));
        Vector3 euler = rotation.eulerAngles;
        //Three.js has different order of rotation for Euler angles, so we have to do that manually
        var rot = Quaternion.AngleAxis(euler.z, Vector3.back) *
                Quaternion.AngleAxis(euler.x, Vector3.right) *
                Quaternion.AngleAxis(euler.y, Vector3.up);
        
        //can only use main thread to assign transform values
        transform.position = position;
        transform.rotation = rot;
    }

    private void OnDestroy()
    {
        httpServer.Stop();
        receiveChannel?.Close();
        peerConnection?.Close();
        WebRTC.Dispose();
    }
}

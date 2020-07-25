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
// using Unity.WebRTC;
using HttpStatusCode = WebSocketSharp.Net.HttpStatusCode;

public class SignallingServer : MonoBehaviour
{
    public int port = 443;
    private HttpServer httpServer;
    // private RTCPeerConnection peerConnection;
    // private RTCDataChannel receiveChannel, sendChannel;

    private Action<Body> SendMessage;
    
    // public RTCAnswerOptions answerOptions = new RTCAnswerOptions()
    // {
    //     iceRestart = false,
    // };
    //
    // private RTCConfiguration configuration = new RTCConfiguration()
    // {
    //     iceServers = new[] {new RTCIceServer() {urls = new[] {"stun.l.google.com:19302"}}}
    // };
    //
    // public RTCDataChannelInit dataChannelInit = new RTCDataChannelInit()
    // {
    //     reliable = true,
    // };
    
    // private void Awake()
    // {
    //     WebRTC.Initialize();
    // }
    public void Start()
    {
        SetupSignallingServer();
        // peerConnection = new RTCPeerConnection(ref configuration);
        // sendChannel = peerConnection.CreateDataChannel("send", ref dataChannelInit);
        // peerConnection.OnDataChannel += channel =>
        // {
        //     receiveChannel = channel;
        //     Debug.Log("Received Data Channel");
        //     // receiveChannel.OnMessage += ReceiveMessage();
        // };
        // peerConnection.OnIceCandidate = candidate =>
        // {
        //     Debug.Log(candidate);
        // };
        // peerConnection.OnIceConnectionChange = state =>
        // {
        //     Debug.Log(state);
        // };
        //
        // StartCoroutine(WebRTC.Update());
    }

    public void SetupSignallingServer()
    {
        try
        {
            Debug.Log("Starting to create WebSocket Server");
            httpServer = new HttpServer(IPAddress.Parse("0.0.0.0"), port, true);
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
            
            // Action<SignallingService> serviceInitializer = delegate(SignallingService service)
            // {
            //     service.MessageReceived = ReceiveMessage;
            //     SendMessage = service.SendData;
            // };
            // httpServer.AddWebSocketService("/signalling", serviceInitializer);
            
            httpServer.SslConfiguration.ServerCertificate =
                new X509Certificate2(Path.Combine(Application.streamingAssetsPath, "ssl", "server.pfx"), "mecal");
            httpServer.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
            httpServer.SslConfiguration.CheckCertificateRevocation = false;

            httpServer.Start();
            Debug.Log("Finished creating WebSocket Server");
        }
        catch (Exception e)
        {
            Debug.Log($"Error Creating WebSocket Server: {e.Message}");
        }
    }

    public void ReceiveMessage(Body body)
    {
        Debug.Log($"Received Message ${body.data}");
        // switch (body.type)
        // {
        //     case "offer":
        //         StartCoroutine(ReceiveOffer(body.data));
        //         break;
        //     case "candidate":
        //         RTCIceCandidate iceCandidate = new RTCIceCandidate() {candidate = body.data};
        //         peerConnection.AddIceCandidate(ref iceCandidate);
        //         break;
        //     case "leave":
        //         peerConnection = new RTCPeerConnection(ref configuration);
        //         break;
        //     default:
        //         Debug.LogError("Invalid Message Type");
        //         break;
        // }
    }

    // private IEnumerator ReceiveOffer(string offer)
    // {
    //     RTCSessionDescription desc = new RTCSessionDescription() { sdp = offer, type = RTCSdpType.Offer };
    //     var op1 = peerConnection.SetRemoteDescription(ref desc);
    //     yield return op1;
    //     var op2 = peerConnection.CreateAnswer(ref answerOptions);
    //     yield return op2;
    //     RTCSessionDescription answerDesc = op2.Desc;
    //     var op3 = peerConnection.SetLocalDescription(ref answerDesc);
    //     yield return op3;
    //     SendMessage(new Body()
    //     {
    //         type = "answer",
    //         data = answerDesc.sdp,
    //     });
    // }
    
    // private void OnDestroy()
    // {
    //     receiveChannel?.Close();
    //     sendChannel?.Close();
    //     peerConnection?.Close();
    //     WebRTC.Dispose();
    // }
}

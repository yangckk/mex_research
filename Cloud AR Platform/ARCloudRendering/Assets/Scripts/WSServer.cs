using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Rendering;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

public class ARCommunication : WebSocketBehavior
{
    public Action<string> MessageReceived = null;

    protected override void OnOpen()
    {
        Debug.Log("Connection Opened");
    }

    protected override void OnClose(CloseEventArgs e)
    {
        Debug.Log("Connection Closed");
    }

    protected override void OnError(ErrorEventArgs e)
    {
        Debug.Log("Connection Error: " + e.Message);
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        if (MessageReceived != null)
            MessageReceived(e.Data);
    }

    public void SendData(byte[] data)
    {
        Send(data);
    }
}

public class WSServer : MonoBehaviour
{
    public Encoding encoding;
    public int port = 80;
    public int quality;
    public RenderTexture cameraTexture;
    public RawImage serverView;

    public ComputeShader encoderShader;

    private WebSocketServer webSocketServer;

    private RenderTexture encodedTexture;
    private Texture2D sendTexture;

    private int encoderKernelHandle;
    private byte[] encodedBytes;

    private Transform transform;
    private Queue<AsyncGPUReadbackRequest> requests = new Queue<AsyncGPUReadbackRequest>();

    private Action<byte[]> SendMessage;
    
    void Start() {
        transform = GetComponent<Transform>();
        Application.runInBackground = true;
        Application.targetFrameRate = Constants.FPS;

        //initialize sizes of all textures (double width if encoding JPG)
        cameraTexture.width = Constants.WIDTH;
        cameraTexture.height = Constants.HEIGHT;
        encodedTexture = new RenderTexture(Constants.WIDTH * 2, Constants.HEIGHT, 0, RenderTextureFormat.Default);
        encodedTexture.enableRandomWrite = true;
        sendTexture = new Texture2D(Constants.WIDTH * (encoding == Encoding.JPG ? 2 : 1), Constants.HEIGHT, TextureFormat.RGBA32, false);

        //Compute Shader handle
        //encoderKernelHandle = encoderShader.FindKernel("ExtractAlpha");
        serverView.texture = cameraTexture;
        
        SetupConnection();
    }

    void SetupConnection()
    {
        int size = (int) Mathf.Pow(2, 16);
        try
        {
            Debug.Log("Starting to create WebSocket Server");
            webSocketServer = new WebSocketServer(port, true);
            var service = new ARCommunication();
            service.MessageReceived = Receive;
            SendMessage = service.SendData;
            webSocketServer.AddWebSocketService<ARCommunication>("/", () => service);
            webSocketServer.SslConfiguration = new ServerSslConfiguration(
                new X509Certificate2(Path.Combine(Application.streamingAssetsPath, "server.pfx")),
                false,
                SslProtocols.Tls12,
                false
            );
            webSocketServer.Start();
            Debug.Log("Finished creating WebSocket Server");
        }
        catch
        {
            Debug.Log("Error Creating WebSocket Server");
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // This asynchronously loads AsyncGPUReadback requests into a queue
        // Each request will pull the RenderTexture from the GPU 
        if (requests.Count < 8)
        {
            switch (encoding)
            {
                case Encoding.JPG:
                    ExtractAlpha();
                    requests.Enqueue(AsyncGPUReadback.Request(encodedTexture));
                    break;
                case Encoding.PNG:
                    requests.Enqueue(AsyncGPUReadback.Request(cameraTexture));
                    break;
                case Encoding.BASE64:
                    requests.Enqueue(AsyncGPUReadback.Request(cameraTexture));
                    break;
            }
        }
        else
            Debug.Log("Too many requests.");

        Graphics.Blit(src, dest); //copy texture on GPU
    }

    void Receive(string json)
    {
        Debug.Log(json);
        Pose pose = JsonUtility.FromJson<Pose>(json);
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

    void Update() {
        while (requests.Count > 0)
        {
            var req = requests.Peek();

            if (req.hasError)
            {
                Debug.Log("GPU readback error detected.");
                requests.Dequeue();
            }
            else if (req.done)
            {
                //encode the texture pulled from the GPU
                if (webSocketServer.IsListening)
                {
                    var buffer = req.GetData<Color32>();
                    sendTexture.SetPixels32(buffer.ToArray());
                    switch (encoding)
                    {
                        case Encoding.JPG:
                            encodedBytes = sendTexture.EncodeToJPG(quality);
                            SendMessage(encodedBytes);
                            break;
                        case Encoding.PNG:
                            encodedBytes = sendTexture.EncodeToPNG();
                            SendMessage(encodedBytes);
                            break;
                        case Encoding.BASE64:
                            break;
                    }
                }
                requests.Dequeue();
            }
            else
            {
                break;
            }
        }
    }

    void ExtractAlpha()
    {
        //Feed in correct parameters to the compute shader
        encoderShader.SetTexture(encoderKernelHandle, "Result", encodedTexture);
        encoderShader.SetTexture(encoderKernelHandle, "cameraFrame", cameraTexture);
        encoderShader.SetInt("width", Constants.WIDTH);
        encoderShader.SetInt("height", Constants.HEIGHT);
        encoderShader.Dispatch(encoderKernelHandle, (Constants.WIDTH*2)/32, Constants.HEIGHT/32, 1);
    }
    
    void OnApplicationQuit()
    {
        webSocketServer.Stop();
    }
}

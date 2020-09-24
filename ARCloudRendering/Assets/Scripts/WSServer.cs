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
using WebSocketSharp.Server;
using HttpStatusCode = WebSocketSharp.Net.HttpStatusCode;

public class WSServer : MonoBehaviour
{
    public bool GPUReadbackEnabled;
    public Encoding encoding;
    public bool secure;
    public int port;
    public int quality;
    public RenderTexture cameraTexture;

    public ComputeShader encoderShader;

    private HttpServer webSocketServer;

    private RenderTexture encodedTexture;
    private Texture2D sendTexture;

    private int encoderKernelHandle;
    private byte[] encodedBytes;

    private Transform transform;
    private Vector3 devicePosition;
    private Quaternion deviceRotation;
    private Queue<AsyncGPUReadbackRequest> requests = new Queue<AsyncGPUReadbackRequest>();
    
    void Start() {
        Application.runInBackground = true;
        Application.targetFrameRate = Constants.FPS;
        
        transform = GetComponent<Transform>();

        //initialize sizes of all textures (double width if encoding JPG)
        cameraTexture.width = Constants.WIDTH;
        cameraTexture.height = Constants.HEIGHT;
        encodedTexture = new RenderTexture(Constants.WIDTH * 2, Constants.HEIGHT, 0, RenderTextureFormat.Default);
        encodedTexture.enableRandomWrite = true;
        sendTexture = new Texture2D(Constants.WIDTH * (encoding == Encoding.JPG ? 2 : 1), Constants.HEIGHT, TextureFormat.RGBA32, false);

        //Compute Shader handle
        encoderKernelHandle = encoderShader.FindKernel("ExtractAlpha");
        
        SetupConnection();
    }

    void SetupConnection()
    {
        try
        {
            Debug.Log("Starting to create WebSocket Server");
            webSocketServer = new HttpServer(port, secure);
            webSocketServer.DocumentRootPath = Path.Combine(Application.streamingAssetsPath, "webxr-client");
            
            // Set the HTTP GET request event.
            webSocketServer.OnGet += (sender, e) =>
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

            Action<ARCommunication> serviceInitializer = service =>
            {
                WebSocketGlobals.MessageReceived = Receive;
                WebSocketGlobals.SendMessage = service.SendData;
            };
            webSocketServer.AddWebSocketService("/AR", serviceInitializer);
            
            if (secure) {
                webSocketServer.SslConfiguration.ServerCertificate =
                    new X509Certificate2(Path.Combine(Application.streamingAssetsPath, "ssl", "server_self.pfx"));
                webSocketServer.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
                webSocketServer.SslConfiguration.CheckCertificateRevocation = false;
            }
            
            webSocketServer.Start();
            Debug.Log("Finished creating WebSocket Server");
        }
        catch (Exception e)
        {
            Debug.Log($"Error Creating WebSocket Server\n{e.Message}");
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (GPUReadbackEnabled)
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
                }
            }
            else
                Debug.Log("Too many requests.");
        }

        Graphics.Blit(src, dest); //copy texture on GPU
    }

    void Receive(string json)
    {
        // Debug.Log(json);
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
        devicePosition = position;
        deviceRotation = rot;
    }

    void Update() {
        transform.position = devicePosition;
        transform.rotation = deviceRotation;

        if (GPUReadbackEnabled)
        {
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
                    var buffer = req.GetData<Color32>();
                    sendTexture.SetPixels32(buffer.ToArray());
                    EncodeAndSend();

                    requests.Dequeue();
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            switch (encoding)
            {
                case Encoding.JPG:
                    ExtractAlpha();
                    RenderTexture.active = encodedTexture;
                    sendTexture.ReadPixels(new Rect(0, 0, Constants.WIDTH * 2, Constants.HEIGHT), 0, 0, false);
                    break;
                case Encoding.PNG:
                    RenderTexture.active = cameraTexture;
                    sendTexture.ReadPixels(new Rect(0, 0, Constants.WIDTH, Constants.HEIGHT), 0, 0, false);
                    break;
            }
            sendTexture.Apply();
            EncodeAndSend();
        }
    }

    void EncodeAndSend()
    {
        if (WebSocketGlobals.Connected)
        {
            switch (encoding)
            {
                case Encoding.JPG:
                    encodedBytes = sendTexture.EncodeToJPG(quality);
                    WebSocketGlobals.SendMessage(encodedBytes);
                    break;
                case Encoding.PNG:
                    encodedBytes = sendTexture.EncodeToPNG();
                    WebSocketGlobals.SendMessage(encodedBytes);
                    break;
            }
            // File.WriteAllBytes(Path.Combine(Application.streamingAssetsPath, "images", $"test.{encoding}"), encodedBytes);
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

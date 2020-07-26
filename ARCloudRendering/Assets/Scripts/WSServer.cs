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
    public int port;

    private HttpServer server;
    
    private Action<Body> SendMessage;
    
    void Start() {
        Application.runInBackground = true;
        Application.targetFrameRate = Constants.FPS;
        
        SetupConnection();
    }

    void SetupConnection()
    {
        try
        {
            Debug.Log("Starting to create WebSocket Server");
            server = new HttpServer(port, true);
            server.DocumentRootPath = Path.Combine(Application.streamingAssetsPath, "webxr-client");
            
            // Set the HTTP GET request event.
            server.OnGet += (sender, e) =>
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
                service.MessageReceived = ReceiveMessage;
                SendMessage = service.SendData;
            };
            server.AddWebSocketService("/signalling", serviceInitializer);
            
            server.Start();
            Debug.Log("Finished creating WebSocket Server");
        }
        catch
        {
            Debug.Log("Error Creating WebSocket Server");
        }
    }

    public void ReceiveMessage(string json)
    {
        Body body = JsonUtility.FromJson<Body>(json);
        Debug.Log($"Received {body.type} message: {body.data}");

        switch (body.type)
        {
            case "offer":
                break;
            case "candidate":
                break;
        }
    }
    
    void OnApplicationQuit()
    {
        server.Stop();
    }
}

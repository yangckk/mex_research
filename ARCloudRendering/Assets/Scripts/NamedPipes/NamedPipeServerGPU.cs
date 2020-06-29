using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Rendering;
using System.IO.Pipes;

public class NamedPipeServerGPU : MonoBehaviour
{
    public static NamedPipeServerGPU Instance;

    public Encoding encoding;
    public string pipeName = "MECAL-";
    public int quality;
    public Transform controlledTransform;
    public RenderTexture cameraTexture;
    public RawImage serverView;

    public ComputeShader encoderShader;

    private NamedPipeClientStream ImageServer;
    private StreamWriter ImageWriter;
    private NamedPipeClientStream PoseServer;
    private StreamReader PoseReader;

    private RenderTexture encodedTexture;
    private Texture2D sendTexture;

    private int encoderKernelHandle;
    private byte[] encodedBytes;

    private bool reading = false;
    private Transform transform;
    private Thread ReadThread;

    private Queue<AsyncGPUReadbackRequest> requests = new Queue<AsyncGPUReadbackRequest>();

    void Start() {
        Instance = this;
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
            //Create a Named Pipe stream that only sends information (for sending encoded image data)
            ImageServer = new NamedPipeClientStream(".", pipeName + "Image", PipeDirection.Out, PipeOptions.WriteThrough);
            ImageServer.Connect();
            Debug.Log("Image Pipe Connection Opened");
            //Create a Named Pipe stream that only receives information (for receiving device pose data)
            PoseServer = new NamedPipeClientStream(".", pipeName + "Pose", PipeDirection.In, PipeOptions.WriteThrough);
            PoseServer.Connect();
            PoseReader = new StreamReader(PoseServer);
            Debug.Log("Pose Pipe Connection Opened");
        }
        catch
        {
            Debug.Log("Error Connecting to named pipe");
        }
    }

    private void Read()
    {
        if (PoseServer.IsConnected)
        {
            if (!reading)
            {
                //Read the buffer and clear the rest of the buffer to avoid the buffer overflowing due to timing issues
                reading = true;
                string json = PoseReader.ReadLine();
                PoseReader.DiscardBufferedData();
                try
                {
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
                    Loom.QueueOnMainThread(() =>
                    {
                        transform.position = position;
                        transform.rotation = rot;
                    });
                }
                catch (ArgumentException e)
                {
                    Debug.Log($"Error in JSON: {json}");
                }

                reading = false;
            }
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

    void Update() {
        Loom.RunAsync(Read); //Read happens forever in the background
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
                if (ImageServer.IsConnected)
                {
                    var buffer = req.GetData<Color32>();
                    sendTexture.SetPixels32(buffer.ToArray());
                    switch (encoding)
                    {
                        case Encoding.JPG:
                            encodedBytes = sendTexture.EncodeToJPG();
                            ImageWriter.Write(encodedBytes);
                            break;
                        case Encoding.PNG:
                            encodedBytes = sendTexture.EncodeToPNG();
                            ImageServer.Write(encodedBytes, 0, encodedBytes.Length);
                            break;
                        case Encoding.BASE64:
                            encodedBytes = sendTexture.EncodeToPNG();
                            ImageWriter.Write(Convert.ToBase64String(encodedBytes));
                            break;
                    }
                    //ImageWriter.Flush();
                    requests.Dequeue();
                }
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
        ReadThread.Abort();
        PoseReader.Close();
        ImageServer.Close();
        PoseServer.Close();
    }
}

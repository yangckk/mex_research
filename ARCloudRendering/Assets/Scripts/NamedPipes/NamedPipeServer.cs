using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Pipes;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class NamedPipeServer : MonoBehaviour
{
    public static NamedPipeServer Instance;

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

    private bool stop = false;

    public Action<byte[]> SendFunc;

    void Start() {
        Instance = this;
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
            ImageWriter = new StreamWriter(ImageServer);
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

    async void Update() {
        ExtractAlpha();

        switch (encoding)
        {
            case Encoding.JPG:
                ExtractAlpha();
                RenderTexture.active = encodedTexture;
                sendTexture.ReadPixels(new Rect(0, 0, Constants.WIDTH * 2, Constants.HEIGHT), 0, 0, false);
                encodedBytes = sendTexture.EncodeToJPG();
                break;
            case Encoding.PNG:
                RenderTexture.active = cameraTexture;
                sendTexture.ReadPixels(new Rect(0, 0, Constants.WIDTH, Constants.HEIGHT), 0, 0, false);
                encodedBytes = sendTexture.EncodeToPNG();
                break;
            case Encoding.BASE64:
                break;
        }

        if (ImageServer.IsConnected)
        {
            await ImageServer.WriteAsync(encodedBytes, 0, encodedBytes.Length);
            await ImageServer.FlushAsync();
        }

        if (PoseServer.IsConnected)
        {
            string json = PoseReader.ReadLine();
            PoseReader.DiscardBufferedData();
            try
            {
                Pose pose = JsonUtility.FromJson<Pose>(json);
                Vector3 position = new Vector3(pose.position.x, pose.position.y, -pose.position.z);
                Quaternion rotation =
                    Quaternion.Inverse(new Quaternion(pose.rotation.x, pose.rotation.y, pose.rotation.z, pose.rotation.w));
                Vector3 euler = rotation.eulerAngles;
                var rot = Quaternion.AngleAxis(euler.z, Vector3.back) *
                          Quaternion.AngleAxis(euler.x, Vector3.right) *
                          Quaternion.AngleAxis(euler.y, Vector3.up);
                transform.position = position;
                transform.rotation = rot;
            }
            catch (ArgumentException e)
            {
                Debug.Log($"Error in JSON: {json}");
            }
        }
    }

    void ExtractAlpha()
    {
        encoderShader.SetTexture(encoderKernelHandle, "Result", encodedTexture);
        encoderShader.SetTexture(encoderKernelHandle, "cameraFrame", cameraTexture);
        encoderShader.SetInt("width", Constants.WIDTH);
        encoderShader.SetInt("height", Constants.HEIGHT);
        encoderShader.Dispatch(encoderKernelHandle, (Constants.WIDTH*2)/32, Constants.HEIGHT/32, 1);
    }

    void OnApplicationQuit()
    {
        stop = true;
        PoseServer.Close();
        PoseReader.Close();
        ImageServer.Close();
        PoseServer.Close();
    }
}

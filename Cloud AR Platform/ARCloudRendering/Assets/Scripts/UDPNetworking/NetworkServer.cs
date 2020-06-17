using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

using LiteNetLib;
using LiteNetLib.Utils;

public class NetworkServer : MonoBehaviour
{
    public Encoding encoding;
    int port = 5000;
    public int quality;
    public Transform controlledTransform;
    public RenderTexture cameraTexture;
    public RawImage serverView;

    public ComputeShader encoderShader;

    private NetManager netManager;
    private EventBasedNetListener netListener;
    private NetPacketProcessor netProcessor;

    private NetPeer client = null;
    
    private RenderTexture encodedTexture;
    private Texture2D sendTexture;

    private int encoderKernelHandle;
    private byte[] encodedBytes;

    private bool stop = false;

    void Start() {
        Application.runInBackground = true;
        Application.targetFrameRate = Constants.FPS;

        Debug.Log("[SERVER] Starting Server");
        netListener = new EventBasedNetListener();
        netListener.ConnectionRequestEvent += OnConnectionRequest;
        netListener.PeerConnectedEvent += OnPeerConnected;
        netListener.NetworkReceiveEvent += OnNetworkReceive;

        netManager = new NetManager(netListener);
        netManager.Start(port);
        netManager.BroadcastReceiveEnabled = true;

        netProcessor = new NetPacketProcessor();
        netProcessor.SubscribeReusable<PositionData>(ReceiveData);

        cameraTexture.width = Constants.WIDTH;
        cameraTexture.height = Constants.HEIGHT;
        encodedTexture = new RenderTexture(Constants.WIDTH * 2, Constants.HEIGHT, 0, RenderTextureFormat.Default);
        encodedTexture.enableRandomWrite = true;
        sendTexture = new Texture2D(Constants.WIDTH * 2, Constants.HEIGHT, TextureFormat.RGBA32, false);

        encoderKernelHandle = encoderShader.FindKernel("ExtractAlpha");

        serverView.texture = cameraTexture;
    }

    void Update() {
        netManager.PollEvents();
        ExtractAlpha();
        
        RenderTexture.active = encodedTexture;
        sendTexture.ReadPixels(new Rect(0, 0, Constants.WIDTH * 2, Constants.HEIGHT), 0, 0, false);

        switch (encoding)
        {
            case Encoding.JPG:
                encodedBytes = sendTexture.EncodeToJPG();
                break;
            case Encoding.PNG:
                encodedBytes = sendTexture.EncodeToPNG();
                break;
            case Encoding.BASE64:
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, sendTexture);
                    encodedBytes = ms.ToArray();
                }
                break;
        }

        SendEncodedFrame(); 
    }

    void ExtractAlpha()
    {
        encoderShader.SetTexture(encoderKernelHandle, "Result", encodedTexture);
        encoderShader.SetTexture(encoderKernelHandle, "cameraFrame", cameraTexture);
        encoderShader.SetInt("width", Constants.WIDTH);
        encoderShader.SetInt("height", Constants.HEIGHT);
        encoderShader.Dispatch(encoderKernelHandle, (Constants.WIDTH*2)/32, Constants.HEIGHT/32, 1);
    }

    void SendEncodedFrame() {
        if (client != null) {
            netProcessor.Send(client, new Frame {bytes = encodedBytes}, DeliveryMethod.ReliableOrdered);
        }
    }

    void ReceiveData(PositionData positionData) {
        controlledTransform.position    = new Vector3(positionData.PositionX, positionData.PositionY, positionData.PositionZ);
        controlledTransform.eulerAngles = new Vector3(positionData.RotationX, positionData.RotationY, positionData.RotationZ);
    }

    void OnConnectionRequest(ConnectionRequest request) {
        request.Accept();
    }

    void OnPeerConnected(NetPeer peer) {
        Debug.Log($"[SERVER] Client Connected ({peer.EndPoint.Address})");
        client = peer;
    }

    void OnNetworkReceive(NetPeer client, NetPacketReader reader, DeliveryMethod method) {
        // Debug.Log(reader.RawData);
        netProcessor.ReadAllPackets(reader, client);
    }

    void OnApplicationQuit() {
        stop = true;
    }
}

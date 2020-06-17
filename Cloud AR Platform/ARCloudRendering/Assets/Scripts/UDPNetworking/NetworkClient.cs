using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;

using LiteNetLib;
using LiteNetLib.Utils;

public class NetworkClient : MonoBehaviour
{
    public Encoding encoding;
    public string IP = "localhost";
    public int port = 5000;
    public Transform clientTransform;
    public RawImage clientView;

    public ComputeShader decoderShader;

    private NetManager netManager;
    private EventBasedNetListener netListener;
    private NetPacketProcessor netProcessor;

    private NetPeer server = null;

    private Texture2D receiveTexture;
    private RenderTexture decodedTexture;

    private int decoderKernelHandle;

    void Start() {        
        Application.runInBackground = true;
        Application.targetFrameRate = Constants.FPS;

        netListener = new EventBasedNetListener();
        netListener.PeerConnectedEvent += OnPeerConnected;
        netListener.NetworkReceiveEvent += OnNetworkReceive;

        netManager = new NetManager(netListener);
        netManager.Start();
        netManager.Connect(IP, port, "");

        netProcessor = new NetPacketProcessor();
        netProcessor.SubscribeReusable<Frame>(ReceiveData);

        receiveTexture = new Texture2D(Constants.WIDTH * 2, Constants.HEIGHT, TextureFormat.RGBA32, false);
        decodedTexture = new RenderTexture(Constants.WIDTH, Constants.HEIGHT, 0, RenderTextureFormat.Default);
        decodedTexture.enableRandomWrite = true;
        decodedTexture.Create();
        decoderKernelHandle = decoderShader.FindKernel("CombineAlpha");

        clientView.texture = decodedTexture;
    }

    void Update() {
        netManager.PollEvents();

        CombineAlpha();
        SendClientTransform();
    }

    void CombineAlpha()
    {
        decoderShader.SetTexture(decoderKernelHandle, "Result", decodedTexture);
        decoderShader.SetTexture(decoderKernelHandle, "encodedFrame", receiveTexture);
        decoderShader.SetInt("width", Constants.WIDTH);
        decoderShader.SetInt("height", Constants.HEIGHT);
        decoderShader.Dispatch(decoderKernelHandle, Constants.WIDTH/32, Constants.HEIGHT/32, 1);
    }

    void SendClientTransform() {
        PositionData data = new PositionData();
        data.PositionX = clientTransform.position.x;
        data.PositionY = clientTransform.position.y;
        data.PositionZ = clientTransform.position.z;
        data.RotationX = clientTransform.eulerAngles.x;
        data.RotationY = clientTransform.eulerAngles.y;
        data.RotationZ = clientTransform.eulerAngles.z;

        if (server != null)
            netProcessor.Send(server, data, DeliveryMethod.ReliableOrdered);
    }

    void OnPeerConnected(NetPeer peer) {
        Debug.Log($"[CLIENT] Connected to Server: ({peer.EndPoint.Address})");
        server = peer;
    } 

    void OnNetworkReceive(NetPeer server, NetPacketReader reader, DeliveryMethod method) {
        netProcessor.ReadAllPackets(reader, server);
    }

    void ReceiveData(Frame frame) {
        Debug.Log($"[CLIENT] Received {frame.bytes.Length} bytes from server");
        switch (encoding) {
            case Encoding.JPG:
            case Encoding.PNG:
                receiveTexture.LoadImage(frame.bytes);
                // receiveTexture.Apply();
                break;
            case Encoding.BASE64:

                break;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.Events;
using WebSocketSharp;
using WebSocketSharp.Server;

public static class Constants {
    public const int WIDTH = 237;
    public const int HEIGHT = 512;
    public const int FPS = 60;
}

public enum Encoding {
    JPG = 0,
    PNG = 1
}

[System.Serializable]
public class Position
{
    public float x, y, z, w;
}

[System.Serializable]
public class RotationQuaternion
{
    public float x, y, z, w;
}

[System.Serializable]
public class Pose
{
    public Position position;
    public RotationQuaternion rotation;
}

public static class WebSocketGlobals
{
    public static Action<string> MessageReceived;
    public static Action<byte[]> SendMessage;
    public static bool Connected;
}

public class ARCommunication : WebSocketBehavior
{
    protected override void OnOpen()
    {
        Debug.Log("Connection Opened");
        WebSocketGlobals.Connected = true;
    }

    protected override void OnClose(CloseEventArgs e)
    {
        Debug.Log("Connection Closed");
        WebSocketGlobals.Connected = false;
    }

    protected override void OnError(ErrorEventArgs e)
    {
        Debug.LogError(e.Message);
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        if (WebSocketGlobals.Connected)
            WebSocketGlobals.MessageReceived(e.Data);
    }

    public void SendData(byte[] data)
    {
        Send(data);
        // Debug.Log("Sending Encoded Image Data length: " + data.Length);
    }
}


//SIGNALLING
[Serializable]
public class Body
{
    public string type;
    public string data;
}

public class SignallingService : WebSocketBehavior
{
    public Action<Body> MessageReceived = null;

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
            MessageReceived(JsonUtility.FromJson<Body>(e.Data));
    }

    public void SendData(Body body)
    {
        string json = JsonUtility.ToJson(body);
        Debug.Log("Sending JSON Data:\n " + json);
        Send(json);
    }
}


//SIGNALLING
[Serializable]
public class Body
{
    public string type;
    public string data;
}

public class SignallingService : WebSocketBehavior
{
    public Action<Body> MessageReceived = null;

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
            MessageReceived(JsonUtility.FromJson<Body>(e.Data));
    }

    public void SendData(Body body)
    {
        string json = JsonUtility.ToJson(body);
        Debug.Log("Sending JSON Data:\n " + json);
        Send(json);
    }
}
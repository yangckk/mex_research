using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
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

public class SignallingService : WebSocketBehavior
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

    public void SendData(Body body)
    {
        Debug.Log($"Sending {body.type} message: " + body.data);
        Send(JsonUtility.ToJson(body));
    }
}

[Serializable]
public class Body
{
    public string type;
    public string data;
}
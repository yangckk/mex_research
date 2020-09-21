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
    public static Action<string> SendMessage;
    public static bool Connected;
}

//SIGNALLING
[Serializable]
public class Type
{
    public string type;
}

[Serializable]
public class SDP
{
    public string type;
    public string sdp;
}

[Serializable]
public class CandidateString
{
    public string candidate;
    public string sdpMid;
    public int sdpMLineIndex;
}

[Serializable]
public class Candidate
{
    public string type;
    public CandidateString candidate;
}

public class SignallingService : WebSocketBehavior
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
        Debug.Log("Connection Error: " + e.Message);
        WebSocketGlobals.Connected = false;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        if (WebSocketGlobals.Connected)
            WebSocketGlobals.MessageReceived(e.Data);
    }

    public void SendData(string data)
    {
        Send(data);
    }
}
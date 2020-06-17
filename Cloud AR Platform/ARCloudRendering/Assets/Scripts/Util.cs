using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public static class Constants {
    public const int WIDTH = 237;
    public const int HEIGHT = 512;
    public const int FPS = 60;
}

public enum Encoding {
    JPG = 0,
    PNG = 1,
    BASE64 = 2
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
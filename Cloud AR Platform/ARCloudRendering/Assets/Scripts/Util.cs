using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using JetBrains.Annotations;
using LiteNetLib.Utils;
using Org.BouncyCastle;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

public class Frame : INetSerializable{
    public byte[] bytes {get; set;}

    public void Serialize(NetDataWriter writer) {
        writer.PutBytesWithLength(bytes);
    }

    public void Deserialize(NetDataReader reader) {
        bytes = reader.GetBytesWithLength();
    }
}

public class PositionData : INetSerializable{
    public float PositionX {get; set;}
    public float PositionY {get; set;}
    public float PositionZ {get; set;}
    public float RotationX {get; set;}
    public float RotationY {get; set;}
    public float RotationZ {get; set;}

    public void Serialize(NetDataWriter writer) {
        writer.Put(PositionX);
        writer.Put(PositionY);
        writer.Put(PositionZ);
        writer.Put(RotationX);
        writer.Put(RotationY);
        writer.Put(RotationZ);
    }

    public void Deserialize(NetDataReader reader) {
        PositionX = reader.GetFloat();
        PositionY = reader.GetFloat();
        PositionZ = reader.GetFloat();
        RotationX = reader.GetFloat();
        RotationY = reader.GetFloat();
        RotationZ = reader.GetFloat();
    }
}

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
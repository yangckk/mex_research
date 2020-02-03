/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using System;


public class Client : MonoBehaviour
{
    public Rigidbody rb;
    public Position.PositionClient client;
    public Vector3 pos;
    public float mag = 100.0f;
    public float pGain = 0.0f;
    public float dGain = 0.0f;
    public float iGain = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        // change ip 164.67.195.73:50051
        Channel channel = new Channel("192.168.1.3:50051", ChannelCredentials.Insecure);
        client = new Position.PositionClient(channel);
    }

    // Update is called once per frame
    async void FixedUpdate()
    {
        pos = rb.position;
        if (pos.y >= 0)
        {
            Vector3 force = await System.Threading.Tasks.Task.Run(() => asyncPositionRequest(pos));
            rb.AddForce(force);
        }
        //Debug.Log("This is being called from FixedUpdate()");
    }

    public Vector3 asyncPositionRequest(Vector3 pos)
    {
        Debug.Log("Called asyncPositionRequest"); 
        var reply = client.SendPosition(new PositionRequest
        {
            X = pos.x,
            Y = pos.y,
            Z = pos.z,
            Kp = pGain,
            Kd = dGain,
            Ki = iGain,
            CientTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow)
        });

        Debug.Log(reply);
        return new Vector3(reply.ActuationForce, 0, 0);
    }
}
*/
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;


public class Client : MonoBehaviour
{
    public Rigidbody rb;
    public Position.PositionClient client;
    public Vector3 pos;
    public float mag = 500.0f;
    // Start is called before the first frame update
    void Start()
    {
        Channel channel = new Channel("164.67.195.73:50051", ChannelCredentials.Insecure);
        client = new Position.PositionClient(channel);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pos = rb.position;
        var reply = client.SendPosition(new PositionRequest
        {
            X = pos.x,
            Y = pos.y,
            Z = pos.z
        });

        Vector3 force = new Vector3(reply.ActuationForce * mag, 0, 0);
        rb.AddForce(force);
        Debug.Log(reply);
    }

}
*/
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;


public class Client : MonoBehaviour
{
    public Rigidbody rb;
    public Position.PositionClient client;
    public Vector3 pos;
    public float mag = 50.0f;
    public float Kp = 0.0f;
    public float Kd = 0.0f;
    public float Ki = 0.0f;
    public string ipaddress;
    //public int id;
    //public float sp;

    public static Vector3 force;
    private static string GetArg(string name)
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 1)
            {
                return args[i + 1];
            }
        }
        return null;
    }


    // Start is called before the first frame update
    void Start()
    {
        // The IP Address should be changed to the IP Address of the server
        //127.0.0.1
        //var output = GetArg("-ip");
        //ipaddress = output.ToString();
        Channel channel = new Channel("172.28.13.72:50051", ChannelCredentials.Insecure);
        client = new Position.PositionClient(channel);

    }

    void FixedUpdate()
    {
        pos = rb.position;
        var reply = client.SendPosition(new PositionRequest
        {
            X = pos.x,
            Y = pos.y,
            Z = pos.z,
            Kp = Kp,
            Kd = Kd,
            Ki = Ki
            //Id = id,
            //Sp = sp
        });

        force = new Vector3(reply.ActuationForce * mag, 0, 0);
        //Vector3 force = System.Threading.Tasks.Task.Run(() => AsyncPositionRequest(pos));
        rb.AddForce(force);
        Debug.Log(reply);
    }

    public Vector3 AsyncPositionRequest(Vector3 pos)
    {
        var reply = client.SendPosition(new PositionRequest
        {
            X = pos.x,
            Y = pos.y,
            Z = pos.z,
            Kp = Kp,
            Kd = Kd,
            Ki = Ki
            //Id = id,
            //Sp = sp
        });

        Debug.Log(reply);
        return new Vector3(reply.ActuationForce, 0, 0);
    }

}
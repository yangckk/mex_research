using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using System;


public class VPSClient : MonoBehaviour
{
    public Rigidbody rb;
    public Position.PositionClient client;
    
    void Start()
    {
        Channel channel = new Channel("164.67.195.73:50051", ChannelCredentials.Insecure);
        client = new Position.PositionClient(channel);
    }

    void FixedUpdate()
    {
        //Vector3 force = await System.Threading.Tasks.Task.Run(() => asyncPositionRequest(pos));
        //rb.AddForce(force);
        //Debug.Log("This is being called from FixedUpdate()");
    }

    //public Vector3 asyncSendPositions(Vector3 pos)
    //{
    //    Debug.Log("Called asyncSendPositions");
    //    var reply = client.SendPosition(new PositionRequest
    //    {
    //        X = pos.x,
    //        Y = pos.y,
    //        Z = pos.z,
    //        Kp = pGain,
    //        Kd = dGain,
    //        Ki = iGain,
    //        ClientTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow)
    //    });

    //    Debug.Log(reply);
    //    return new Vector3(reply.ActuationForce, 0, 0);
    //}
}

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
        Channel channel = new Channel("192.168.172.153:50051", ChannelCredentials.Insecure);
        client = new Position.PositionClient(channel);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pos = rb.position;
        var reply = client.SendPosition(new PositionRequest {
            X = pos.x,
            Y = pos.y,
            Z = pos.z
        });

        Vector3 force = new Vector3(reply.ActuationForce * mag, 0, 0);
        rb.AddForce(force);
        Debug.Log(reply);
    }

}

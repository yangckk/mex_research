using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using System;


public class Client : MonoBehaviour
{
    public Rigidbody rb;
    public Position.PositionClient client;
    public string address = "164.67.195.73:50051";

    public float x = 0;
    public float velocity = 0;
    public float theta = 0;
    public float angularVelocity = 0;

    // Start is called before the first frame update
    void Start()
    {
        Channel channel = new Channel(address, ChannelCredentials.Insecure);
        client = new Position.PositionClient(channel);
    }

    // Update is called once per frame
    async void FixedUpdate()
    {
        x = rb.position.x;
        velocity = rb.velocity.x;
        HingeJoint joint = rb.GetComponent<HingeJoint>();
        theta = Mathf.Deg2Rad * joint.angle;
        angularVelocity = Mathf.Deg2Rad * joint.velocity;

        Vector3 force = await System.Threading.Tasks.Task.Run(() => asyncPositionRequest());
        rb.AddForce(force);
        //Debug.Log("This is being called from FixedUpdate()");
    }

    public Vector3 asyncPositionRequest()
    {
        Debug.Log("Sent Position Request");
        Debug.Log(string.Format("X: {0}, X_dot: {1}, Theta: {2}, Theta_dot: {3}", x, velocity, theta, angularVelocity));
        var reply = client.SendPosition(new PositionRequest
        {
            X = x,
            XDot = velocity,
            Theta = theta,
            ThetaDot = angularVelocity,
        });

        Debug.Log(reply);
        return new Vector3(reply.Force, 0, 0);
    }
}

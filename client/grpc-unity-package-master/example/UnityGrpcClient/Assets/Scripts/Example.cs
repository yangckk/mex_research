using System;
using Grpc.Core;
using Helloworld;
using UnityEngine;
using UnityEngine.UI;

public class Example : MonoBehaviour {

    public Text Response;

    public void Send()
    {
        try
        {
            var channel = new Channel("0.0.0.0:50051", ChannelCredentials.Insecure);

            var client = new Greeter.GreeterClient(channel);

            var reply = client.SayHello(new HelloRequest { Name = "Unity", Timestampclient = System.DateTime.Now.ToString("HH:mm:ss.fff") });

            Response.text = reply.Message;
            channel.ShutdownAsync().Wait();
        }
        catch (Exception e)
        {
            Response.text = "exception: " + e.Message;
        }
    }
}

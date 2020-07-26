using System.Collections;
using System.Collections.Generic;
using Gst;
using UnityEngine;
using Gst.App;
using Gst.WebRTC;

public class GStreamer : MonoBehaviour
{
    public void Start()
    {
        AppSink AppSink;
        AppSrc AppSrc;
        Element Pipeline, DecodeBin, VideoConvert, Encoder, Parser, Queue;
        
        AppSrc = new AppSrc("src");
        DecodeBin = ElementFactory.Make("decodebin");
        VideoConvert = ElementFactory.Make("videoconvert");
        Encoder = ElementFactory.Make("x264enc");
        Parser = ElementFactory.Make("h264parser");
        Queue = ElementFactory.Make("queue");
        AppSink = new AppSink("sink");
        
        Pipeline = new Pipeline("pipeline");
    }
}

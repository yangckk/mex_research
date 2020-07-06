using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using FFmpeg.AutoGen;
using FFmpeg;
using FFmpeg.AutoGen.Native;

public unsafe class VideoEncoder : MonoBehaviour
{
    private Stream inputStream;
    private Stream outputStream;

    private AVCodec* codec;
    private AVCodecContext* context = null;
    private AVFrame* frame;

    private int width, height, fps;

    public unsafe VideoEncoder(ref Stream inputStream, int fps, int width, int height, ref Stream outputStream)
    {
        RegisterFFmpegBinaries();
        this.inputStream = inputStream;
        this.outputStream = outputStream;
        this.width = width;
        this.height = height;
        this.fps = fps;
        
        //Find codec encoder
        var codecId = AVCodecID.AV_CODEC_ID_VP9;
        codec = ffmpeg.avcodec_find_encoder(codecId);
        if (codec == null) throw new InvalidOperationException("Codec not found.");

        context = ffmpeg.avcodec_alloc_context3(codec);
        context->width = width;
        context->height = height;
        context->time_base = new AVRational {num = 1, den = fps};
        context->pix_fmt = AVPixelFormat.AV_PIX_FMT_YUVA420P;
        ffmpeg.av_opt_set(context->priv_data, "preset", "ultrafast", 0);

        ffmpeg.avcodec_open2(context, codec, null);
        
        
    }
    
    internal static void RegisterFFmpegBinaries()
    {
        var ffmpegBinaryPath = Path.Combine(Application.streamingAssetsPath);
        ffmpeg.RootPath = ffmpegBinaryPath;
    }
    
    public void Encode(byte[] bytes)
    {
        
    }
}
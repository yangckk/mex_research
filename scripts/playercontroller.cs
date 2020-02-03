using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class playercontroller : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 position;

    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;

    public float randomForce = 1000f;

    public Stopwatch stopWatch = new Stopwatch();
    public System.TimeSpan ts;
    bool checkTS = false;
    List<double> firstlist = new List<double>();
    public double ret = 0;
    public double avg = 0;
    public double count = 0;
    static string fileName = @"C:\Users\Dayuen\Desktop\" + DateTime.Now.ToString("MM-dd-hh-mm-ss") +".txt";
    

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.AddForce(Client.force);
       // UnityEngine.Debug.Log(Client.force);
        
        if (Input.GetKey("d") )
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            stopWatch.Reset();
            stopWatch.Start();
            checkTS = true;
          
        }

        if (Input.GetKey("a") )
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            stopWatch.Reset();
            stopWatch.Start();
            checkTS = true;
            
        }

        Vector3 pos = GameObject.Find("Sphere").transform.position;

        if(pos.y >= 4.554 && checkTS && !Input.GetKey("a") && !Input.GetKey("d"))
        {
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            StreamWriter sw = new StreamWriter(fileName, true);

            UnityEngine.Debug.Log(ts);
            sw.WriteLine(ts.ToString());
            firstlist.Add(ts.TotalMilliseconds);
            //sw.WriteLine(ts.TotalMilliseconds);
            sw.Dispose();
            stopWatch.Reset();
            checkTS = false;
            
        }


     
        position = rb.position;
        UnityEngine.Debug.Log(position);
    }
    void OnApplicationQuit()
    {
       /*for (int i=0; i <firstlist.Count();i++)
        {
            new StreamWriter(fileName, true).WriteLine(firstlist[i]);
            new StreamWriter(fileName, true).Dispose();
        }*/
            avg = firstlist.Average();
        count = firstlist.Count();
            ret = Math.Sqrt(firstlist.Average(v => Math.Pow(v - avg, 2)));
        StreamWriter sw = new StreamWriter(fileName, true);
        sw.WriteLine("Number of data points: " + count.ToString());
        sw.WriteLine("Mean: " + avg.ToString());

        sw.WriteLine("Standard Deviation: "+ret.ToString());
        sw.Dispose();
    }
}

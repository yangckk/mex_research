using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KaiserPID: MonoBehaviour
{
    private Rigidbody rb;
    

    const float setpoint = 0.0f;
    public float kp, ki, kd,dT;
    float prevError = 0.0f;
    float integral = 0.0f;
    float currE;
    public float theta;
    public float now;
    


    float PID(float currError,float Kp, float Kd, float Ki,  float dt)
    {
        
        integral += currError * dt;
        float derivative = (currError - prevError) / dt;
        float actuator = Kp * currError + Kd * derivative + Ki * integral;
        prevError = currError;
        return actuator;
    }
    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody>();

    }
    void FixedUpdate()
    {
        var ra = GameObject.Find("Sphere").transform.position;
        var rr = GameObject.Find("Cart").transform.position;
        theta = GameObject.Find("Sphere").GetComponent<Transform>().transform.localEulerAngles.z;
        if (theta > 0 && theta < 90) // too much to the left
        {
            now = -theta;

        }
        else if (theta < 360 && theta > 270)// too much to the right
            now = 360 - theta;
            
        dT = Time.deltaTime;
        currE = now;
       
        rb.AddForce(new Vector3(PID(currE, kp, kd, ki, dT), 0, 0) );
    }


}
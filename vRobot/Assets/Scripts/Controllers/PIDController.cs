using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Test PID controller
public class PIDController : MonoBehaviour
{
    public Pendulum pendulum;

    const float setpoint = 0.0f;
    public float kp, ki, kd, dT;
    float prevError = 0.0f;
    float integral = 0.0f;
    float currE;
    public float nonMotionRange;
    public float theta;
    public float force;
    

    float PID(float currError, float Kp, float Kd, float Ki, float dt)
    {

        integral += currError * dt;
        float derivative = (currError - prevError) / dt;
        float actuator = Kp * currError + Kd * derivative + Ki * integral;
        prevError = currError;
        return actuator;
    }

    void Update()
    {
        theta = pendulum.joint.angle;
        if (Mathf.Abs(theta) > nonMotionRange)
        {
            dT = Time.deltaTime;
            currE = theta;

            force = PID(currE, kp, kd, ki, dT);
            //Debug.Log(string.Format("Theta: {0}\tError: {1}\tPID Velocity: {2}", theta, currE, force));
            pendulum.baseRigidbody.AddForce(new Vector3(force, 0, 0), ForceMode.Force);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

//handles user input for movement
public class PlayerController : MonoBehaviour
{
    public float speed;
    
    private Pendulum pendulum;

    void Awake()
    {
        pendulum = GetComponent<Pendulum>();
    }

    void FixedUpdate()
    {        
        if (Input.GetKey(KeyCode.RightArrow) )
        {
            pendulum.leverRigidbody.AddForce(Vector3.right * speed);     
        }

        if (Input.GetKey(KeyCode.LeftArrow) )
        {
            pendulum.leverRigidbody.AddForce(Vector3.left * speed);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            pendulum.leverRigidbody.AddForce(Vector3.back * speed);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            pendulum.leverRigidbody.AddForce(Vector3.forward * speed);
        }

    }
}

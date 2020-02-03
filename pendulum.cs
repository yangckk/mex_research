using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pendulum : MonoBehaviour
{
    public float speed = 100f;
    private Rigidbody rb;
    //private Rigidbody ra;
    public float theta;


    // Use this for initialization
    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

        var ra = GameObject.Find("Sphere").transform.position;
        var rr= GameObject.Find("Cart").transform.position;
        theta = GameObject.Find("Sphere").GetComponent<Transform>().transform.localEulerAngles.z;

        if (theta>0 && theta <90) // too much to the left
        {
            //float xx = ra.x-rr.x;
            //float xx = Input.GetAxis("Horizontal");
            //float z = Input.GetAxis("Vertical");
            rb.AddForce(new Vector3(-theta, 0, 0) * speed  );
            
        }
        else if (theta <360 &&theta >90)// too much to the right
            rb.AddForce(new Vector3((360-theta), 0, 0) * speed);
        //else if (theta< 0 ) // too much to the left
        //{
        //float xx = ra.x-rr.x;
        //float xx = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");
        //  rb.AddForce(new Vector3(theta, 0, 0) * speed );
        //}

    }
}

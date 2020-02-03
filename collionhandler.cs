using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class collionhandler : MonoBehaviour
{
    public playercontroller movement;
    public Transform transform1;
    public Transform transform2;
    Collision col;
    public Vector3 offset;
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            movement.enabled = false;
        }

        if (collisionInfo.collider.tag == "Road1")
        {
            transform2.position = transform1.position + offset;
        }

        if (collisionInfo.collider.tag == "Road2")
        {
            transform1.position = transform2.position + offset;
        }
    }
    void Update()
    {
        OnCollisionEnter(col);
    }
}

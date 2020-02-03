using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    

    float mainSpeed = 100.0f; 
    float sensitivity = 0.25f; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); 

    void Update()
{
        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-lastMouse.y * sensitivity, lastMouse.x * sensitivity, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        if (!Input.GetKey(KeyCode.Escape))
        {
            transform.eulerAngles = lastMouse;
        }
        lastMouse = Input.mousePosition;

        Vector3 p = GetBaseInput();
        
        p = p * mainSpeed*Time.deltaTime;
        Vector3 newPosition = transform.position;
        transform.Translate(p);

    }

    private Vector3 GetBaseInput()
    { 
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
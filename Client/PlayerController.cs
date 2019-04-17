using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rb;
    public Vector3 position;

    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;

    public float randomForce = 1000f;

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(0,0,forwardForce * Time.deltaTime);

        if ( Input.GetKey("d"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Random.Range(1, 100) == 1)
        {
            rb.AddForce(randomForce * Time.deltaTime, 0, 0);
        }
        position = rb.position;
        Debug.Log(position);
    }
}

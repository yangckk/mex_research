using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class size : MonoBehaviour
{
    public float X;
    public float Y;
    public float Z;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(X, Y, Z);
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

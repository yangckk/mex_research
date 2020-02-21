using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles pendulum properties and components
public class Pendulum : MonoBehaviour
{
    const float LEVER_ROD_RADIUS = 0.2f;

    public GameObject pendulumBase;
    public GameObject pendulumLever;
    public GameObject pendulumRod;

    public float leverLength;

    public float hingeLimitMin;
    public float hingeLimitMax;

    public JointLimits limits;

    internal Rigidbody baseRigidbody;
    private Vector3 leverPosition;
    internal Rigidbody leverRigidbody;
    internal HingeJoint joint;
    
    void Awake()
    {
        //get physics components
        baseRigidbody = pendulumBase.GetComponent<Rigidbody>();
        leverRigidbody = pendulumLever.GetComponent<Rigidbody>();
        leverPosition = pendulumLever.transform.position;
        joint = pendulumBase.GetComponent<HingeJoint>();
    }

    void Start()
    {
        //modify the range of angles the pendulum can swing
        JointLimits limits = joint.limits;
        limits.min = hingeLimitMin;
        limits.max = hingeLimitMax;
        joint.limits = limits;
        joint.useLimits = true;

        //modify the length of the lever
        pendulumLever.transform.localPosition = new Vector3(0, leverLength + 1, 0); //1 is the height of the base
        pendulumRod.transform.localPosition = new Vector3(0, -leverLength, 0);
        pendulumRod.transform.localScale = new Vector3(LEVER_ROD_RADIUS, leverLength, LEVER_ROD_RADIUS);
        //pendulumLever.transform.localPosition = new Vector3(0, (leverLength/2)+1, 0);
        //pendulumLever.transform.localScale = new Vector3(LEVER_ROD_RADIUS, leverLength/2, LEVER_ROD_RADIUS);
        joint.autoConfigureConnectedAnchor = true;

    }
}

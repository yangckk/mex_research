using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public PlayerController movement;
    public Transform transform1;
    public Transform transform2;

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
}

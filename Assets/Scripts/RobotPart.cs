using UnityEngine;

public class RobotPart : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        float force = collision.relativeVelocity.magnitude;

        if (force > 1f)
        {
            Debug.Log(name + " hit " + collision.gameObject.name + " with impact force: " + force.ToString("F1"));
        }
    }
}
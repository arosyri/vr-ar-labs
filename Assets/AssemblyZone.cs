using UnityEngine;

public class AssemblyZone : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<RobotPart>() != null)
        {
            Debug.Log("Part entered the assembly zone: " + other.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<RobotPart>() != null)
        {
            Debug.Log("Part left the assembly zone: " + other.name);
        }
    }
}
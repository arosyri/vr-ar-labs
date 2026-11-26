using System.Collections.Generic;
using UnityEngine;

public class SnapZone : MonoBehaviour
{
    public Transform snapPoint;
    public float snapSpeed = 8f;
    private readonly HashSet<Rigidbody> snapped = new HashSet<Rigidbody>();
    void OnTriggerStay(Collider other)
    {
        RobotPart part = other.GetComponentInParent<RobotPart>();
        if (part == null) return;

        Rigidbody rb = part.GetComponent<Rigidbody>();
        if (rb == null) return;

        if (rb.isKinematic)
        {
            snapped.Remove(rb);
            return;
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.position = Vector3.Lerp(rb.position, snapPoint.position, Time.fixedDeltaTime * snapSpeed);
        rb.rotation = Quaternion.Slerp(rb.rotation, snapPoint.rotation, Time.fixedDeltaTime * snapSpeed);

        if (Vector3.Distance(rb.position, snapPoint.position) < 0.03f && snapped.Add(rb))
        {
            Debug.Log("Part snapped into place: " + part.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponentInParent<Rigidbody>();
        if (rb != null) snapped.Remove(rb);
    }
}
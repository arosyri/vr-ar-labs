using UnityEngine;

public class AssemblySocket : MonoBehaviour
{
    public string requiredPartName = "RobotLegLeft";

    public AssemblySocket[] requires;

    public Renderer ghost;

    public float snapSpeed = 10f;

    public bool IsFilled { get; private set; }

    private Rigidbody current;

    private readonly Color emptyColor = new Color(0.2f, 0.8f, 1f, 0.35f);
    private readonly Color blockedColor = new Color(0.6f, 0.6f, 0.6f, 0.15f);
    private readonly Color filledColor = new Color(0.2f, 1f, 0.3f, 0.6f);

    bool PrerequisitesDone()
    {
        foreach (AssemblySocket s in requires)
        {
            if (!s.IsFilled) return false;
        }
        return true;
    }

    void Update()
    {
        if (ghost == null) return;

        if (IsFilled) ghost.material.color = filledColor;
        else if (!PrerequisitesDone()) ghost.material.color = blockedColor;
        else ghost.material.color = emptyColor;
    }

    void OnTriggerStay(Collider other)
    {
        RobotPart part = other.GetComponentInParent<RobotPart>();
        if (part == null || !part.name.StartsWith(requiredPartName)) return;

        Rigidbody rb = part.GetComponent<Rigidbody>();
        if (rb == null) return;

        if (rb.isKinematic)
        {
            if (current == rb) Release();
            return;
        }

        if (!PrerequisitesDone()) return;
        if (current != null && current != rb) return;

        current = rb;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = Vector3.Lerp(rb.position, transform.position, Time.fixedDeltaTime * snapSpeed);
        rb.rotation = Quaternion.Slerp(rb.rotation, transform.rotation, Time.fixedDeltaTime * snapSpeed);

        if (!IsFilled && Vector3.Distance(rb.position, transform.position) < 0.03f)
        {
            IsFilled = true;
            Debug.Log("Part installed: " + part.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponentInParent<Rigidbody>();
        if (rb != null && rb == current) Release();
    }

    void Release()
    {
        if (IsFilled) Debug.Log("Part removed from socket: " + requiredPartName);
        IsFilled = false;
        current = null;
    }
}
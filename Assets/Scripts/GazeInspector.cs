using UnityEngine;

public class GazeInspector : MonoBehaviour
{
    public float maxDistance = 5f;

    private PartInfo current;
    private string lastHitName = "";

    void Update()
    {
        Transform head = Camera.main.transform;
        PartInfo found = null;

        Debug.DrawRay(head.position, head.forward * maxDistance, Color.red);

        if (Physics.Raycast(head.position, head.forward, out RaycastHit hit, maxDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            found = hit.collider.GetComponentInParent<PartInfo>();

            if (hit.collider.name != lastHitName)
            {
                lastHitName = hit.collider.name;
                Debug.Log("Gaze hit: " + hit.collider.name + " (PartInfo found: " + (found != null) + ")");
            }
        }
        else
        {
            lastHitName = "";
        }

        if (found != current)
        {
            if (current != null) current.Hide();
            if (found != null) found.Show();
            current = found;
        }
    }
}
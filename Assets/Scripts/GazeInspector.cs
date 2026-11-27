using System.Linq;
using UnityEngine;

public class GazeInspector : MonoBehaviour
{
    public float maxDistance = 5f;

    private PartInfo current;

    void Update()
    {
        Transform head = Camera.main.transform;
        PartInfo found = null;

        RaycastHit[] hits = Physics.RaycastAll(head.position, head.forward, maxDistance, ~0, QueryTriggerInteraction.Ignore)
                                   .OrderBy(h => h.distance).ToArray();

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.GetComponentInParent<OVRCameraRig>() != null) continue;

            found = hit.collider.GetComponentInParent<PartInfo>();
            break; 
        }

        if (found != current)
        {
            if (current != null) current.SetGazed(false);
            if (found != null) found.SetGazed(true);
            current = found;
        }
    }
}
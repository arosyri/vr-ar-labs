using UnityEngine;

public class DangerZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            Debug.LogWarning("WARNING! You have entered the danger zone!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            Debug.Log("You have left the danger zone.");
        }
    }
}
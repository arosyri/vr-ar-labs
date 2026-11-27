using UnityEngine;
using TMPro;

public class PartInfo : MonoBehaviour
{
    public string title = "Robot head";
    [TextArea] public string description = "Contains the sensors and the control module of the robot.";

    public GameObject card;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public float height = 0.45f;

    private Rigidbody rb;
    private bool gazed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (card == null || titleText == null || descriptionText == null)
        {
            Debug.LogError("PartInfo on " + name + ": Card, Title Text or Description Text is not assigned!");
            return;
        }

        titleText.text = title;
        descriptionText.text = description;
        card.SetActive(false);
    }

    public void SetGazed(bool value)
    {
        gazed = value;
    }

    void LateUpdate()
    {
        if (card == null) return;

        bool held = rb != null && rb.isKinematic;
        bool shouldShow = gazed || held;

        if (card.activeSelf != shouldShow) card.SetActive(shouldShow);
        if (!shouldShow) return;

        Transform head = Camera.main.transform;
        Vector3 pos = transform.position + Vector3.up * height;
        card.transform.position = pos;
        card.transform.rotation = Quaternion.LookRotation(pos - head.position);
    }
}
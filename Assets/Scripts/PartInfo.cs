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

    void Start()
    {
        titleText.text = title;
        descriptionText.text = description;
        card.SetActive(false);
    }

    public void Show() { card.SetActive(true); }
    public void Hide() { card.SetActive(false); }

    void LateUpdate()
    {
        if (!card.activeSelf) return;

        Transform head = Camera.main.transform;
        Vector3 pos = transform.position + Vector3.up * height;
        card.transform.position = pos;
        card.transform.rotation = Quaternion.LookRotation(pos - head.position);
    }
}
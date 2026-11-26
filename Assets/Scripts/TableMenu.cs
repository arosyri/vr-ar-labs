using UnityEngine;
using UnityEngine.InputSystem;

public class TabletMenu : MonoBehaviour
{
    public GameObject tablet;
    public float distance = 1.0f;
    private bool visible;
    void Start()
    {
        SetVisible(false);
    }

    void Update()
    {
        bool menuPressed = OVRInput.GetDown(OVRInput.Button.Start);
        bool tabPressed = Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame;

        if (menuPressed || tabPressed)
        {
            SetVisible(!visible);
            if (visible) PlaceInFrontOfUser();
        }
    }

    void SetVisible(bool value)
    {
        visible = value;
        tablet.SetActive(value);
    }

    void PlaceInFrontOfUser()
    {
        Transform head = Camera.main.transform;

        Vector3 forward = head.forward;
        forward.y = 0f;
        forward.Normalize();

        tablet.transform.position = head.position + forward * distance + Vector3.down * 0.1f;
        tablet.transform.rotation = Quaternion.LookRotation(forward);
    }
}
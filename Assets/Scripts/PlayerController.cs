using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public float mouseSensitivity = 0.1f;

    private float pitch = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 mouse = Mouse.current.delta.ReadValue() * mouseSensitivity;
        transform.Rotate(0f, mouse.x, 0f);
        pitch = Mathf.Clamp(pitch - mouse.y, -80f, 80f);
        Camera.main.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        float x = 0f, z = 0f;
        if (Keyboard.current.wKey.isPressed) z += 1f;
        if (Keyboard.current.sKey.isPressed) z -= 1f;
        if (Keyboard.current.dKey.isPressed) x += 1f;
        if (Keyboard.current.aKey.isPressed) x -= 1f;

        Vector3 move = (transform.right * x + transform.forward * z) * speed * Time.deltaTime;
        transform.position += move;
    }
}
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class MouseLook : MonoBehaviour
{
    public Transform yaw;    // Player/CameraRoot
    public Transform pitch;  // Player/CameraRoot/CameraTarget
    public float sensX = 180f, sensY = 180f;
    public float minPitch = -40f, maxPitch = 60f;

    float yawAngle, pitchAngle;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mx = 0f, my = 0f;

#if ENABLE_INPUT_SYSTEM
        if (Mouse.current != null)
        {
            Vector2 d = Mouse.current.delta.ReadValue();
            mx = d.x; my = d.y;
        }
#else
        mx = Input.GetAxis("Mouse X");
        my = Input.GetAxis("Mouse Y");
#endif

        yawAngle += mx * sensX * Time.deltaTime;
        pitchAngle -= my * sensY * Time.deltaTime;
        pitchAngle = Mathf.Clamp(pitchAngle, minPitch, maxPitch);

        if (yaw) yaw.localRotation = Quaternion.Euler(0f, yawAngle, 0f);
        if (pitch) pitch.localRotation = Quaternion.Euler(pitchAngle, 0f, 0f);
    }
}

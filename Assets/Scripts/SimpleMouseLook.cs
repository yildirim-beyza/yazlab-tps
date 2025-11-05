using UnityEngine;
using Unity.Cinemachine; // CM3

[RequireComponent(typeof(CinemachinePanTilt))]
public class SimpleMouseLook : MonoBehaviour
{
    public float sensitivityX = 200f;
    public float sensitivityY = 150f;
    public float minY = -40f, maxY = 70f;

    CinemachinePanTilt panTilt;
    float pan, tilt;

    void Awake()
    {
        panTilt = GetComponent<CinemachinePanTilt>();
        pan = panTilt.PanAxis.Value;
        tilt = panTilt.TiltAxis.Value;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float dx = Input.GetAxis("Mouse X"); // klasik input
        float dy = Input.GetAxis("Mouse Y");

        pan += dx * sensitivityX * Time.deltaTime;
        tilt -= dy * sensitivityY * Time.deltaTime;
        tilt = Mathf.Clamp(tilt, minY, maxY);

        panTilt.PanAxis.Value = pan;
        panTilt.TiltAxis.Value = tilt;
    }
}

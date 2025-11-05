using UnityEngine;

// --- NAMESPACE UYUMU ---
// Unity 6 / CM3'te genelde Unity.Cinemachine kullanýlýr.
#if CINEMACHINE_3_0_0_OR_NEWER || UNITY_6000_0_OR_NEWER
using Unity.Cinemachine;
using VCam = Unity.Cinemachine.CinemachineCamera;
#else
using Cinemachine;
using VCam = Cinemachine.CinemachineVirtualCamera;
#endif

public class CameraSwitcher : MonoBehaviour
{
    [Header("Cameras")]
    public VCam normalCam;
    public VCam aimCam;

    [Header("Settings")]
    public KeyCode aimKey = KeyCode.Mouse1; // sað týk
    public float normalFOV = 50f;
    public float aimFOV = 40f;

    void Start()
    {
        SetPriority(normalCam, 20);
        SetPriority(aimCam, 0);

        SetFOV(normalCam, normalFOV);
        SetFOV(aimCam, aimFOV);
    }

    void Update()
    {
        bool aiming = Input.GetKey(aimKey);

        if (aiming)
        {
            SetPriority(aimCam, 20);
            SetPriority(normalCam, 0);
        }
        else
        {
            SetPriority(normalCam, 20);
            SetPriority(aimCam, 0);
        }
        // Geçiþ yumuþamasýný Cinemachine Brain'deki Default Blend belirler.
    }

    // -----------------------

    void SetPriority(VCam cam, int p)
    {
        if (cam != null) cam.Priority = p;
    }

    void SetFOV(VCam cam, float fov)
    {
        if (cam == null) return;
#if CINEMACHINE_3_0_0_OR_NEWER || UNITY_6000_0_OR_NEWER
        cam.Lens.FieldOfView = fov;     // CM3
#else
        cam.m_Lens.FieldOfView = fov;   // CM2
#endif
    }
}

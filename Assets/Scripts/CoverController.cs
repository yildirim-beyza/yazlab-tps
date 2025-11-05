using UnityEngine;
using System.Linq;

public class CoverController : MonoBehaviour
{
    [Header("Refs")]
    public CharacterController cc;           // Player'ın CC'si
    public Transform cameraTarget;           // Player/CameraRoot/CameraTarget
    public LayerMask coverMask = ~0;         // Duvar/Environment katmanları

    [Header("Input")]
    public KeyCode coverKeyPrimary = KeyCode.LeftControl;
    public KeyCode coverKeyAlt = KeyCode.Q;   // alternatif tuş (deneme için)

    [Header("Detect")]
    public float detectDistance = 1.3f;      // duvara max mesafe
    public float snapGap = 0.02f;            // duvara yaklaşırken kalan boşluk
    [Tooltip("Yukarı (Vector3.up) ile dot < bu değer olmalı (dikey yüzey). 0.2 iyi.")]
    public float wallMinDot = 0.2f;

    [Header("Crouch & Camera")]
    public float crouchHeight = 1.2f;
    public float standHeight = 1.8f;
    public Vector3 cameraTargetStand = new Vector3(0, 1.40f, 0);
    public Vector3 cameraTargetCover = new Vector3(0, 1.10f, 0);
    public float camLerp = 12f;

    [Header("Slide")]
    public float slideSpeed = 3.5f;

    [Header("Debug")]
    public bool showDebug = true;

    // state
    public bool isInCover { get; private set; }
    public Vector3 coverNormal { get; private set; }

    string lastFail = "";

    void Reset()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Kamera hedef yüksekliği
        if (cameraTarget)
        {
            var target = isInCover ? cameraTargetCover : cameraTargetStand;
            cameraTarget.localPosition = Vector3.Lerp(cameraTarget.localPosition, target, Time.deltaTime * camLerp);
        }

        // Teşhis çizgileri
        if (showDebug) DrawDebugRays();

        // Giriş/Çıkış tuşu
        if (Input.GetKeyDown(coverKeyPrimary) || Input.GetKeyDown(coverKeyAlt))
        {
            if (!isInCover) TryEnterCover();
            else ExitCover();
        }

        // Cover'dayken duvarı kaybedersek veya açı bozulursa çık
        if (isInCover)
        {
            if (!StillHasWall(out var hit))
            {
                if (showDebug) Debug.Log("COVER: lost wall → EXIT");
                ExitCover();
                return;
            }

            coverNormal = hit.normal;
            AlignAndStickToWall(hit);
        }
    }

    void TryEnterCover()
    {
        if (!StillHasWall(out var hit))
        {
            Fail("No wall in front (increase Detect Distance, check Collider & Mask).");
            return;
        }

        float dot = Vector3.Dot(hit.normal, Vector3.up); // yataya yakınsa 1'e yaklaşır
        if (dot >= wallMinDot)
        {
            Fail($"Surface too horizontal (dot={dot:F2} ≥ {wallMinDot}). Face a vertical wall.");
            return;
        }

        // hizala ve gir
        coverNormal = hit.normal;
        AlignAndStickToWall(hit);
        SetCrouch(true);
        isInCover = true;
        if (showDebug) Debug.Log("COVER: ENTER");
    }

    void ExitCover()
    {
        isInCover = false;
        SetCrouch(false);
        if (showDebug) Debug.Log("COVER: EXIT");
    }

    bool StillHasWall(out RaycastHit hit)
    {
        Vector3 origin = transform.position + Vector3.up * 1.2f;  // göğüs hizası
        Vector3 dir = transform.forward;

        // tam ileri + hafif sol/sağ tarama
        if (Physics.Raycast(origin, dir, out hit, detectDistance, coverMask, QueryTriggerInteraction.Ignore))
            return true;

        Vector3 dirL = Quaternion.Euler(0, -10f, 0) * dir;
        Vector3 dirR = Quaternion.Euler(0, +10f, 0) * dir;

        if (Physics.Raycast(origin, dirL, out hit, detectDistance, coverMask, QueryTriggerInteraction.Ignore))
            return true;
        if (Physics.Raycast(origin, dirR, out hit, detectDistance, coverMask, QueryTriggerInteraction.Ignore))
            return true;

        return false;
    }

    void AlignAndStickToWall(RaycastHit hit)
    {
        // Yatay normal ile bakış (duvara paralel)
        Vector3 flatNormal = hit.normal; flatNormal.y = 0f; flatNormal.Normalize();
        if (flatNormal.sqrMagnitude > 0.0001f)
            transform.rotation = Quaternion.LookRotation(-flatNormal, Vector3.up);

        // cc.radius kadar geri çek
        float back = (cc ? cc.radius : 0.3f) + snapGap;
        Vector3 targetPos = hit.point + hit.normal * back;
        targetPos.y = transform.position.y;
        transform.position = targetPos;
    }

    void SetCrouch(bool crouch)
    {
        if (!cc) return;
        float h = crouch ? crouchHeight : standHeight;
        cc.height = h;
        cc.center = new Vector3(0, h * 0.5f, 0);
    }

    public Vector3 ConstrainMove(Vector3 wishWorld)
    {
        if (!isInCover) return wishWorld;
        Vector3 tangent = Vector3.Cross(Vector3.up, coverNormal).normalized; // duvar boyunca
        float side = Vector3.Dot(wishWorld, tangent);
        return tangent * side * slideSpeed;
    }

    void Fail(string msg)
    {
        lastFail = msg;
        if (showDebug) Debug.LogWarning("COVER FAIL: " + msg);
    }

    void DrawDebugRays()
    {
        Vector3 origin = transform.position + Vector3.up * 1.2f;
        Vector3 dir = transform.forward;
        Vector3 dirL = Quaternion.Euler(0, -10f, 0) * dir;
        Vector3 dirR = Quaternion.Euler(0, +10f, 0) * dir;
        Debug.DrawRay(origin, dir * detectDistance, Color.cyan);
        Debug.DrawRay(origin, dirL * detectDistance, Color.magenta);
        Debug.DrawRay(origin, dirR * detectDistance, Color.magenta);
    }

    // Ekranda durum göstergesi (geçici)
    void OnGUI()
    {
        if (!showDebug) return;
        string s = $"COVER: {(isInCover ? "ON" : "OFF")}\nLastFail: {lastFail}";
        GUI.Label(new Rect(10, 10, 600, 60), s);
    }
}

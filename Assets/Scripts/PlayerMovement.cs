using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float acceleration = 12f;     // hýzlanma-yavaþlama yumuþatma

    [Header("Jump & Gravity")]
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float stickToGround = -2f;    // zeminde tutuþ

    [Header("Refs")]
    public CoverController cover;        // Inspector’dan Player’daki CoverController’ý sürükle

    CharacterController cc;
    Vector3 velocity;        // sadece dikey hýz
    Vector3 currentMove;     // yumuþatýlmýþ yatay hýz

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // --- 1) INPUT ---
        float h = 0f, v = 0f;
        bool jumpPressed = false;
        bool sprintHeld = false;

#if ENABLE_INPUT_SYSTEM
        var kb = Keyboard.current;
        if (kb != null)
        {
            h = (kb.aKey.isPressed ? -1f : 0f) + (kb.dKey.isPressed ? 1f : 0f);
            v = (kb.sKey.isPressed ? -1f : 0f) + (kb.wKey.isPressed ? 1f : 0f);
            jumpPressed = kb.spaceKey.wasPressedThisFrame;
            sprintHeld = kb.leftShiftKey.isPressed;
        }
#else
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        jumpPressed = Input.GetButtonDown("Jump");
        sprintHeld  = Input.GetKey(KeyCode.LeftShift);
#endif

        // --- 2) Kameraya göre yön ---
        Transform cam = Camera.main.transform;
        Vector3 camF = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camR = Vector3.Scale(cam.right, new Vector3(1, 0, 1)).normalized;
        Vector3 wishDir = (camF * v + camR * h).normalized;

        // --- 3) Cover kýsýtý (siper modunda duvar boyunca kay) ---
        if (cover != null && cover.isInCover)
        {
            wishDir = cover.ConstrainMove(wishDir);
            jumpPressed = false; // cover’da zýplama yok
        }

        // --- 4) Hýz / ivme ---
        float targetSpeed = moveSpeed * (sprintHeld ? sprintMultiplier : 1f);
        Vector3 targetMove = wishDir * targetSpeed;
        currentMove = Vector3.Lerp(currentMove, targetMove, acceleration * Time.deltaTime);

        // --- 5) Zemin & zýplama ---
        bool grounded = cc.isGrounded;
        if (grounded && velocity.y < 0f) velocity.y = stickToGround;

        if (grounded && jumpPressed)
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);

        // --- 6) Yerçekimi ---
        velocity.y += gravity * Time.deltaTime;

        // --- 7) Uygula ---
        Vector3 motion = currentMove;
        motion.y = velocity.y;
        cc.Move(motion * Time.deltaTime);
    }
}

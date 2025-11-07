using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float acceleration = 12f;

    [Header("Jump & Gravity")]
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float stickToGround = -2f;

    CharacterController cc;
    Vector3 velocity;
    Vector3 currentMove;

    void Awake() => cc = GetComponent<CharacterController>();

    void Update()
    {
        // === 1) Girdi ===
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Kamera yönüne göre hareket
        Vector3 camF = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camR = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 wishDir = (camF * v + camR * h).normalized;

        // === 2) Hız ===
        float targetSpeed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f);
        Vector3 targetMove = wishDir * targetSpeed;
        currentMove = Vector3.Lerp(currentMove, targetMove, acceleration * Time.deltaTime);

        // === 3) Zemin & zıplama ===
        bool grounded = cc.isGrounded;
        if (grounded && velocity.y < 0f) velocity.y = stickToGround;
        if (grounded && Input.GetButtonDown("Jump"))
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);

        // === 4) Yerçekimi ===
        velocity.y += gravity * Time.deltaTime;

        // === 5) Uygula ===
        cc.Move((currentMove + new Vector3(0, velocity.y, 0)) * Time.deltaTime);
    }
}

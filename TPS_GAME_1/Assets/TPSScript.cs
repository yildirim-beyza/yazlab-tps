using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TPSMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public float moveSpeed = 5f;
    public float turnSmoothTime = 0.1f;

    private Rigidbody rb;
    private float turnSmoothVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        // Yalpalamay� �nle
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        // Eski input sistemi ile WASD al�m�
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.MovePosition(rb.position + moveDir.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
    // --- A�A�I EKLE ---

    // Karakterin map d���na d��mesini ve �arp��malarda saplanmas�n� engeller
    private float minYPos = -5f;   // karakter bu Y�nin alt�na d��erse geri teleporter
    private Vector3 safePosition;

    void LateUpdate()
    {
        // E�er karakter havadaysa veya d��meye ba�lam��sa bile
        // her frame'de son g�venli konumu kaydet
        if (transform.position.y > minYPos)
            safePosition = transform.position;

        // Rigidbody �ok �iddetli �arp��malarda f�rlarsa d��meyi engelle
        if (transform.position.y < minYPos)
        {
            Debug.Log("Map d���na ��kt�, g�venli konuma d�n�yor...");
            rb.linearVelocity = Vector3.zero;
            transform.position = safePosition + Vector3.up * 1f;
        }
    }

    // �arp��ma fizi�ini yumu�at (duvar/�ad�r f�rlatmalar�n� engeller)
    void OnCollisionStay(Collision collision)
    {
        // Dikey hareketi s�f�rla (Rigidbody a�a�� saplanmas�n)
        if (rb.linearVelocity.y < 0)
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
    }

}

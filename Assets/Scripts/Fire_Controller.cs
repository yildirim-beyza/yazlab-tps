using UnityEngine;

public class Fire_Controller : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 40f;

    void Update()
    {
#if ENABLE_INPUT_SYSTEM
        if (UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame)
#else
        if (Input.GetMouseButtonDown(0))
#endif
            Shoot();
    }

    void Shoot()
    {
        if (!firePoint || !bulletPrefab) return;

        GameObject b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if (b.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.useGravity = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        Destroy(b, 3f);
    }
}

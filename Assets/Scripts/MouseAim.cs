using UnityEngine;

public class MouseAim : MonoBehaviour
{
    public Transform firePoint;
    public LayerMask groundMask;   // Ground katmaný (Plane/zemin)

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Yer düzlemi yerine katmanlý raycast (daha güvenli)
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundMask))
        {
            Vector3 look = hit.point - transform.position;
            look.y = 0f;
            if (look.sqrMagnitude > 0.001f)
            {
                // Karakteri fare yönüne döndür
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(look),
                    20f * Time.deltaTime
                );

                // Merminin çýkýþ yönünü ayný hedefe çevir
                if (firePoint) firePoint.forward = look.normalized;
            }
        }
    }
}

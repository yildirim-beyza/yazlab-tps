using UnityEngine;

public class AimOrient : MonoBehaviour
{
    public Transform target;      // AimTarget
    public Transform firePoint;   // Player/FirePoint
    public float turnSpeed = 20f;

    void Update()
    {
        if (!target || !firePoint) return;

        // --- sadece yatayda karakteri hedefe döndür ---
        Vector3 flatDir = target.position - transform.position;
        flatDir.y = 0f;
        if (flatDir.sqrMagnitude > 0.0001f)
        {
            Quaternion bodyRot = Quaternion.LookRotation(flatDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, bodyRot, turnSpeed * Time.deltaTime);
        }

        // --- firePoint tam hedefe baksýn ---
        Vector3 dir = (target.position - firePoint.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        firePoint.rotation = Quaternion.Slerp(firePoint.rotation, lookRot, turnSpeed * Time.deltaTime);
    }
}

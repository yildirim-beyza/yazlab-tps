using UnityEngine;

public class CenterAimTarget : MonoBehaviour
{
    public float maxDistance = 200f;
    public LayerMask aimMask = ~0; // Her þeyi vur (istersen Ground/Enemy katmaný ver)

    void LateUpdate()
    {
        var cam = Camera.main;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out var hit, maxDistance, aimMask))
            transform.position = hit.point;
        else
            transform.position = cam.transform.position + cam.transform.forward * maxDistance;

        Debug.DrawRay(cam.transform.position, cam.transform.forward * 20f, Color.yellow);
    }
}

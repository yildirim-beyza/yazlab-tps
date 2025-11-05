using UnityEngine;

public class CrosshairTargetFollow : MonoBehaviour
{
    public float followSpeed = 5f;  // Kameranýn takip edeceði hýz

    void Update()
    {
        // Fare pozisyonundan dünyaya doðru ýþýn at
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Zemine çarptýðý yeri bul
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            // Hedef noktaya yumuþak geçiþ
            transform.position = Vector3.Lerp(transform.position, hit.point, followSpeed * Time.deltaTime);
        }
    }
}

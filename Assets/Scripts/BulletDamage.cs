using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject); // mermi çarptýðýnda yok olsun
    }
}

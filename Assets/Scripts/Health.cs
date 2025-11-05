using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp = 1;
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.attachedRigidbody != null &&
            other.collider.attachedRigidbody.gameObject.name.Contains("Bullet"))
        {
            hp--;
            if (hp <= 0) gameObject.SetActive(false);
        }
    }
}

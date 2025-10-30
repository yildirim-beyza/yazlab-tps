using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public event Action<int, int> OnHealthChanged; // (current, max)
    public event Action OnDied;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    void Die()
    {
        OnDied?.Invoke();
        gameObject.SetActive(false);
    }
}

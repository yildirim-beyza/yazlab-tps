using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public event Action<int, int> OnHealthChanged;
    public event Action OnDied;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // --- DEBUG TUÞLARI ---
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(10);
            Debug.Log($"{gameObject.name} took 10 damage. Current health: {currentHealth}");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Heal(10);
            Debug.Log($"{gameObject.name} healed 10. Current health: {currentHealth}");
        }
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
        Debug.Log($"{gameObject.name} died!");
    }

    [ContextMenu("Debug: Take 10 Damage")]
    void DebugDamage10() => TakeDamage(10);

    [ContextMenu("Debug: Heal 10")]
    void DebugHeal10() => Heal(10);
}

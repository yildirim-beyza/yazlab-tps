using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterSelfAsEnemy : MonoBehaviour
{
    void Start() => TryRegister();

    void OnEnable() => TryRegister();

    void TryRegister()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogWarning($"[Enemy] GameManager bulunamadý, yeniden denenecek: {gameObject.name}");
            Invoke(nameof(TryRegister), 0.2f);
            return;
        }

        GameManager.Instance.RegisterEnemy(gameObject);
        Debug.Log($"[Enemy] Kaydedildi: {gameObject.name}");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLose : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Oyuncuyu devre dýþý býrak -> GameManager Lose açar
            Debug.Log("[TEST] K basýldý -> Player devre dýþý");
            gameObject.SetActive(false);
        }
    }
}

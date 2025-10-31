using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnemySelfDestruct : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            Destroy(gameObject); // Kendini sil -> kalan düþman 0 ise Win açýlýr
    }
}

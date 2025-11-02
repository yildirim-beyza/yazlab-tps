using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Ammo))]
public class DebugShoot : MonoBehaviour
{
    Ammo ammo;

    void Awake() => ammo = GetComponent<Ammo>();

    void Update()
    {
        // Sol týk = 1 mermi harca
        if (Input.GetMouseButtonDown(0)) ammo.SpendOne();

        // R = reload
        if (Input.GetKeyDown(KeyCode.R)) ammo.Reload();
    }
}

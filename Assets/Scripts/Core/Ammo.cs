using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [Header("Ammo Ayarlarý")]
    public int clipSize = 15;         // Þarjör kapasitesi
    public int currentClip = 15;      // Oyuna dolu þarjörle baþlasýn
    public int reserveAmmo = 15;      // Yedekteki mermiler (toplam 45 mermi)
    public float reloadTime = 1.4f;   // Yeniden doldurma süresi (saniye)
    public bool isReloading = false;

    public event Action<int, int, int> OnAmmoChanged; // currentClip, clipSize, reserveAmmo

    void Start()
    {
        Raise(); // oyun baþlarken UI’yý güncelle
    }

    public bool CanShoot() => !isReloading && currentClip > 0;

    public void SpendOne()
    {
        if (!CanShoot()) return;

        currentClip--;
        Raise();
    }

    public void Reload()
    {
        if (isReloading) return;
        if (currentClip == clipSize) return;
        if (reserveAmmo <= 0) return;

        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        int need = clipSize - currentClip;
        int toLoad = Mathf.Min(need, reserveAmmo);

        currentClip += toLoad;
        reserveAmmo -= toLoad;
        isReloading = false;

        Raise();
    }

    public void AddReserve(int amount)
    {
        reserveAmmo += Mathf.Max(0, amount);
        Raise();
    }

    void Raise() => OnAmmoChanged?.Invoke(currentClip, clipSize, reserveAmmo);
}

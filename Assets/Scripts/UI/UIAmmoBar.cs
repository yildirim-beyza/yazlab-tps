using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class UIAmmoBar : MonoBehaviour
{
    public Ammo targetAmmo;
    public Image fillImage;   
    public Text ammoText;     

    void OnEnable()
    {
        if (Application.isPlaying && targetAmmo != null)
            targetAmmo.OnAmmoChanged += UpdateUI;

        if (targetAmmo != null)
            UpdateUI(targetAmmo.currentClip, targetAmmo.clipSize, targetAmmo.reserveAmmo);
    }

    void OnDisable()
    {
        if (Application.isPlaying && targetAmmo != null)
            targetAmmo.OnAmmoChanged -= UpdateUI;
    }

    void OnValidate()
    {
        if (targetAmmo != null)
            UpdateUI(targetAmmo.currentClip, targetAmmo.clipSize, targetAmmo.reserveAmmo);
    }

    public void UpdateUI(int currentClip, int clipSize, int reserve)
    {
        // Bar doluluðu
        if (fillImage)
        {
            float ratio = clipSize > 0 ? (float)currentClip / clipSize : 0f;
            fillImage.fillAmount = ratio;

            // Renkler (mutlak eþikler)
            if (currentClip <= 3)
                fillImage.color = Color.red;
            else if (currentClip <= 7)
                fillImage.color = new Color(1f, 0.6f, 0f); // turuncu
            else
                fillImage.color = new Color(1f, 0.84f, 0f); // sarý
        }

        // Metin(ler)
        if (ammoText)
            ammoText.text = $"{currentClip} / {clipSize}  (+{reserve})";
    }

}


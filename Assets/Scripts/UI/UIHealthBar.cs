using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Health targetHealth;
    public Image fillImage;   // Type = Filled, Fill Method = Horizontal
    public Text healthText;  

    void Start()
    {
        if (targetHealth != null)
        {
            targetHealth.OnHealthChanged += UpdateUI;
            UpdateUI(targetHealth.currentHealth, targetHealth.maxHealth);
        }
    }

    void UpdateUI(int current, int max)
    {
        if (fillImage) fillImage.fillAmount = (float)current / max;
        if (healthText) healthText.text = $"{current} / {max}";
    }

    void OnDestroy()
    {
        if (targetHealth != null)
            targetHealth.OnHealthChanged -= UpdateUI;
    }
}

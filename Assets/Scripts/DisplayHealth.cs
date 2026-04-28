using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;

    public void SetHealth(uint maxHealth)
    {
        // Set max health
        healthSlider.maxValue = maxHealth;
    }

    public void UpdateHealth(uint health)
    {
        // Set display to current health
        healthSlider.value = health;
    }
}

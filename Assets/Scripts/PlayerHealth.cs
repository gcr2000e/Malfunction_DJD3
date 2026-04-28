using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField]
    private uint maxHealth;
    private uint currentHealth;

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private bool invincible = false;

    private void Start()
    {
        currentHealth = maxHealth;

        SetHealth(maxHealth);
        UpdateHealth(currentHealth);
    }

    public void Heal(uint healing)
    {
        // Check if below full hp
        if (currentHealth < maxHealth)
        {
            currentHealth += healing;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            UpdateHealth(currentHealth);
        }
    }
    public void Damage(uint damage)
    {
        // Check if player is invincible or dead
        if (!invincible && currentHealth > 0)
        {
            // Prevent health cicling around
            if (currentHealth < damage)
            {
                OnDeath();
            }
            else
            {
                currentHealth -= damage;
            }
        }
        UpdateHealth(currentHealth);
    }

    private void OnDeath()
    {
        // Set Health to 0
        currentHealth = 0;

        // Do Death sequence here
    }
    public void SetHealth(uint maxHealth)
    {
        healthSlider.maxValue = maxHealth;
    }

    public void UpdateHealth(uint health)
    {
        healthSlider.value = health;
    }
}

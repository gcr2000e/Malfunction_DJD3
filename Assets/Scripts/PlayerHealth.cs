using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField]
    private uint maxHealth;
    private uint currentHealth;

    private DisplayHealth healthDisplay;

    public uint MaxHealth 
        { get { return maxHealth; } }
    public uint CurrentHealth
        { get { return currentHealth; } }

    private bool hasHealthDisplay = false;

    [SerializeField]
    private bool invincible = false;

    private void Start()
    {
        // Set current health to match max health
        currentHealth = maxHealth;
        
        // Get the health display script
        healthDisplay = GetComponent<DisplayHealth>();

        if (healthDisplay != null)
        {
            // Check just in case the player doesn't need the script
            hasHealthDisplay = true;
            // Set max health
            healthDisplay.SetHealth(maxHealth);
            // Update current health to match
            healthDisplay.UpdateHealth(currentHealth);
        }
    }

    public void Heal(uint healing)
    {
        // Check if below full hp
        if (currentHealth < maxHealth)
        {
            currentHealth += healing;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            // Update display
            if (hasHealthDisplay)
                healthDisplay
                    .UpdateHealth(currentHealth);
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
            // Update display
            if (hasHealthDisplay)
                healthDisplay
                    .UpdateHealth(currentHealth);
    }

    private void OnDeath()
    {
        // Set Health to 0
        currentHealth = 0;

        // Do Death sequence here
    }
}

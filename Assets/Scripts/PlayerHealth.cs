using UnityEngine;

public class PlayerHealth : IHealth
{
    [SerializeField]
    private uint maxHealth;
    private uint currentHealth;

    public override uint MaxHealth 
        { get { return maxHealth; } }
    public override uint CurrentHealth
        { get { return currentHealth; } }

    [SerializeField]
    private bool invincible = false;

    private void Start()
    {
        // Set current health to match max health
        currentHealth = maxHealth;
    }

    public override void Heal(uint healing)
    {
        // Check if below full hp
        if (currentHealth < maxHealth)
        {
            currentHealth += healing;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
        }
    }
    public override void Damage(uint damage)
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
    }

    private void OnDeath()
    {
        // Set Health to 0
        currentHealth = 0;

        // Do Death sequence here
    }
}

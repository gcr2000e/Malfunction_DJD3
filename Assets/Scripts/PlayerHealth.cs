using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField]
    private uint maxHealth;
    private uint currentHealth;

    [SerializeField]
    private bool invincible = false;

    public void Heal(uint healing)
    {
        // Check if below full hp
        if (currentHealth < maxHealth)
        {
            currentHealth += healing;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
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
    }

    private void OnDeath()
    {
        // Set Health to 0
        currentHealth = 0;

        // Do Death sequence here
    }
}

using UnityEngine;

public class PlayerHealth : IHealth
{
    [SerializeField]
    private bool invincible = false;

    public override void Heal(uint healing)
    {
        // Check if below full hp
        if (currentHealth < base.maxHealth)
        {
            currentHealth += healing;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
        }
    }
    public override void Damage(uint damage)
    {
        if (!invincible)
        {
            base.Damage(damage);
        }
    }

    protected override void OnDeath()
    {
        // Set Health to 0
        currentHealth = 0;

        // Do Death sequence here
    }
}

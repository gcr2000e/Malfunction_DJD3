using UnityEngine;

public abstract class IHealth : MonoBehaviour
{
    [SerializeField]
    protected uint maxHealth;
    public uint MaxHealth
    { get { return maxHealth; } }

    protected uint currentHealth;
    public  uint CurrentHealth
    { get { return currentHealth; } }

    public virtual void Damage(uint damage)
    {
        // Check if dead
        if (currentHealth > 0)
        {
            // Prevent health cicling around
            if (currentHealth <= damage)
            {
                OnDeath();
            }
            else
            {
                currentHealth -= damage;
            }
        }
    }

    public abstract void Heal(uint healing);

    protected abstract void OnDeath();

    protected virtual void Start()
    {
        // Set current health to match max health
        currentHealth = maxHealth;
    }
}

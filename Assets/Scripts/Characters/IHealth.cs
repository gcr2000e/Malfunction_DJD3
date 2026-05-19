using UnityEngine;

public abstract class IHealth : MonoBehaviour
{
    [SerializeField]
    protected int maxHealth;
    public int MaxHealth
    { get { return maxHealth; } }

    protected int currentHealth;
    public  int CurrentHealth
    { get { return currentHealth; } }

    public virtual void Damage(int damage)
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

    public abstract void Heal(int healing);

    protected abstract void OnDeath();

    protected virtual void Start()
    {
        // Set current health to match max health
        currentHealth = maxHealth;
    }
}

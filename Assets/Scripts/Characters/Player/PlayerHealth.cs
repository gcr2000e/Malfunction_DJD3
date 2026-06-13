using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerHealth : IHealth
{
    [SerializeField]
    private bool invincible = false;

    private bool invincibilityCheat = false;

    public override void Heal(int healing)
    {
        // Check if below full hp
        if (currentHealth < base.maxHealth)
        {
            currentHealth += healing;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
        }
    }
    public override void Damage(int damage)
    {
        if (!invincibilityCheat 
            && !invincible)
        {
            base.Damage(damage);
        }
    }

    public void IncreaseMaxHealth(int addedHealth)
    {
        maxHealth += addedHealth;
        Heal(addedHealth);
    }

    protected override void OnDeath()
    {
        // Set Health to 0
        currentHealth = 0;

        // Do Death sequence here
        SceneManager.LoadScene("MainMenu");
    }

    public void SetHealth(int health, int maxHealth)
    {
        currentHealth = health;
        this.maxHealth = maxHealth;
    }

    private void Update()
    {
        // Invencibility Cheat
        if (InputSystem.actions["InvincibilityCheat"]
            .WasPressedThisFrame())
        {
            invincibilityCheat = !invincibilityCheat;
        }
    }
}

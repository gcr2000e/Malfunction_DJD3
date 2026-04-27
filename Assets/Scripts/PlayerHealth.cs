using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField]
    private uint maxHealth;
    private uint currentHealth;

    [SerializeField]
    private bool invincible = false;

    public void Heal()
    {
        if (currentHealth < maxHealth)
        {

        }
    }
    public void Damage()
    {
        if (!invincible)
        {

        }
    }
}

using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    private float attackBonus = 1.5f;
    [SerializeField]
    private int healthBonus = 50;

    private PlayerCombat pCombat;
    private PlayerHealth pHealth;

    public void SelectAttack()
    {
        pCombat.Upgrade(attackBonus);
    }

    public void SelectHealth()
    {
        pHealth.IncreaseMaxHealth(healthBonus);
    }
}

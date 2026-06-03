using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private Damage hitbox;
    [SerializeField]
    private int attackPower;

    private float attackMultiplierUpgrade = 1;
    public float AtkBonus
    { get { return attackMultiplierUpgrade; } }
    private float attackMultiplierBuff = 1;

    [Header("AnimationControl")]
    [SerializeField]
    private bool recalculateAttack;
    [SerializeField]
    private bool canAttack = true;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (InputSystem.actions["Attack"].WasPressedThisFrame()
            && canAttack)
            Attack();
        if (recalculateAttack)
            CalculateAttack();
    }

    public void CalculateAttack()
    {
        // Calculate damage
        int damage = Mathf.RoundToInt(
            attackPower * 
            attackMultiplierUpgrade * 
            attackMultiplierBuff
            );
        // Change damage on the hitbox
        hitbox.SetDamage(damage);
        // Reset triggers on animator
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Dodge");
    }

    public void Upgrade(float upgrade)
    {
        attackMultiplierUpgrade *= upgrade;
    }

    public void Buff(float buff, float duration)
    {
        attackMultiplierBuff = buff;
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }
}



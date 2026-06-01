using UnityEngine;

public class MeleeEnemy : IEnemy
{
    protected override void Start()
    {
        base.Start();
        // Set the damage to match attack strenght
        GetComponentInChildren<Damage>()
            .SetDamage(attackStrenght);
    }

    protected override void Attack()
    {
        // Do attack animation
        animator.SetBool("CanAttack", true);
    }

    protected override void DoMovement()
    {
        // Move in desired direction
        cc.Move(moveSpeed * Time.deltaTime * model.transform.forward);
        // Do move anim
        animator.SetBool("CanAttack", false);
    }
}
using UnityEngine;

public class MeleeEnemy : IEnemy
{
    protected override void Attack()
    {
        // Do attack animation
        animator.SetBool("CanAttack", true);
    }

    protected override void DoMovement()
    {
        // Move in desired direction
        rb.linearVelocity = model.transform.forward * moveSpeed;
        // Do move anim
        animator.SetBool("CanAttack", false);
    }
}

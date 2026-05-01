using UnityEngine;

public class EnemyHealth : IHealth
{
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    public override void Heal(uint healing)
    {
        // Not needed for now
    }

    public override void Damage(uint damage)
    {
        base.Damage(damage);
        animator.SetTrigger("Stagger");
    }

    protected override void OnDeath()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;

public class EnemyHealth : IHealth
{
    private Animator animator;
    private bool isAlive;
    public bool IsAlive 
    { get { return isAlive; } }

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    public override void Heal(int healing)
    {
        // Not needed for now
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        animator.SetTrigger("Stagger");
    }

    protected override void OnDeath()
    {
        animator.SetTrigger("Death");
    }

    public void LoadEnemy(bool isAlive)
    {
        if (!isAlive)
        {
            OnDeath();
        }
    }
}

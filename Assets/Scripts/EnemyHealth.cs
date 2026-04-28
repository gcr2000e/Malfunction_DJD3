using UnityEngine;

public class EnemyHealth : IHealth
{
    public override void Heal(uint healing)
    {
        // Not needed for now
    }

    protected override void OnDeath()
    {
        Destroy(gameObject);
    }
}

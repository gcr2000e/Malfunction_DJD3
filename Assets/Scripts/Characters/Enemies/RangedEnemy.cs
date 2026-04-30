using UnityEngine;

public class RangedEnemy : IEnemy
{
    [Header("Ranged")]
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform gunPoint;

    [SerializeField]
    private uint shotForce;

    protected override void Attack()
    {
        // Spawn a projectile
        GameObject bullet = Instantiate(
            bulletPrefab, 
            gunPoint.position, 
            Quaternion.LookRotation(transform.forward)
            );
        // Add force to bullet
        bullet.GetComponent<Rigidbody>()
            .AddForce(transform.forward * shotForce);
        // Set the bullet's damage
        bullet.GetComponent<Damage>()
            .SetDamage(attackStrenght);
    }

    protected override void DoMovement()
    {
        // No movement for now
    }
}

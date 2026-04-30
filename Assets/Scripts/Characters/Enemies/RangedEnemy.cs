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

    [SerializeField]
    private float shotCooldown;
    private float timeLastShot;

    protected override void Attack()
    {
        if (Time.time >= timeLastShot + shotCooldown)
        {
            // Spawn a projectile
            GameObject bullet = Instantiate(
                bulletPrefab,
                gunPoint.position,
                Quaternion.LookRotation(transform.forward)
                );
            // Make sure it's active
            bullet.SetActive(true);
            // Add force to bullet
            bullet.GetComponent<Rigidbody>()
                .AddForce(model.transform.forward * shotForce);
            // Set the bullet's damage
            bullet.GetComponent<Damage>()
                .SetDamage(attackStrenght);
            // Set time last shot
            timeLastShot = Time.time;
        }
    }

    protected override void DoMovement()
    {
        // No movement for now
    }
}

using UnityEngine;

public class RangedEnemy1 : IEnemy1
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

    private Transform player;

    protected override void Start()
    {
        base.Start(); // MUITO IMPORTANTE
        player = GameObject.Find("Player (1)").transform;
    }
    protected override void Attack()
    {
        if (Time.time >= timeLastShot + shotCooldown)
        {
            Vector3 direction = player.position - gunPoint.position;
            direction.Normalize();

            GameObject bullet = Instantiate(
                bulletPrefab,
                gunPoint.position,
                Quaternion.LookRotation(direction)
            );

            bullet.SetActive(true);

            bullet.GetComponent<Damage>()?.SetDamage(attackStrenght);

            timeLastShot = Time.time;
        }
    }
 
    protected override void DoMovement()
    {
        // No movement for now
    }
}

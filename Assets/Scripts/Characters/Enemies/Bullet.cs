using UnityEngine;

public class Bullet : Damage
{
    [SerializeField]
    private float maxLifeTime;
    private float lifeTime;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Destroy(gameObject);
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= maxLifeTime)
            Destroy(gameObject);
    }
}

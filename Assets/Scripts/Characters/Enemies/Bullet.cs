using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float maxLifeTime;
    private float lifeTime;

    private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= maxLifeTime)
            Destroy(gameObject);
    }
}

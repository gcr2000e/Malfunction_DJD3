using UnityEngine;

public abstract class IPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        OnPickup(other.gameObject);

        Destroy(gameObject);
    }

    protected abstract void OnPickup(GameObject player);
}

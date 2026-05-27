using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField]
    private int value;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = 
            other.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.Heal(value);
            Destroy(gameObject);
        }
    }
}

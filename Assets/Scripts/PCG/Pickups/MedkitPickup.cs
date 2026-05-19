using UnityEngine;

public class MedkitPickup : IPickup
{
    [SerializeField]
    private int healAmount = 25;

    protected override void OnPickup(GameObject player)
    {
        PlayerHealth playerHealth =
            player.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
        }
    }
}

using UnityEngine;
using System.Linq;

public class RoomControl : MonoBehaviour
{
    [SerializeField]
    private DoorControl[] doors;

    [SerializeField]
    private IEnemy[] enemies;

    private void OnTriggerEnter(Collider other)
    {
        // Check if it is colliding with the player themselves
        if (other.GetComponent<PlayerMovement>() != null)
        {
            // Close doors
            foreach (DoorControl door in doors)
                door.Close();
        }
    }

    // Change to use events
    private void Update()
    {
        if (enemies.All(enemy => enemy == null))
        {
            foreach (DoorControl door in doors)
                door.Open();
            // Disable Game Object to avoid redoing the command
            gameObject.SetActive(false);
        }
    }
}

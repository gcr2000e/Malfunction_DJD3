using UnityEngine;
using System.Linq;

public class RoomControl : MonoBehaviour
{
    [SerializeField]
    private DoorControl entrance, exit;

    [SerializeField]
    private IEnemy[] enemies;

    private void OnTriggerEnter(Collider other)
    {
        // Check if it is colliding with the player themselves
        if (other.GetComponent<PlayerMovement>() != null)
        {
            // Make it so game can't be saved while in combat
            FindAnyObjectByType<SaveSystem>()
                .SetSaveStatus(false);
            // Close entrance door
            entrance.Close();
        }
    }

    private void Update()
    {
        if (enemies.All(enemy => enemy == null))
        {
            exit.Open();
            // Autosave when combat is over
            FindAnyObjectByType<SaveSystem>()
                .AutoSave();
            // Disable Game Object to avoid redoing the command
            gameObject.SetActive(false);
        }
    }
}

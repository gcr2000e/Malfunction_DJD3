using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    public GameObject SpawnPlayer(Transform spawnPoint)
    {
        if (playerPrefab == null ||
            spawnPoint == null)
            return null;

        return Instantiate(
            playerPrefab,
            spawnPoint.position,
            spawnPoint.rotation);
    }
}

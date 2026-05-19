using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    public void SpawnEnemies(RoomData room)
    {
        if (room == null ||
            room.enemySpawnPoints == null)
            return;

        foreach (Transform spawnPoint in room.enemySpawnPoints)
        {
            if (spawnPoint == null)
                continue;

            Instantiate(
                enemyPrefab,
                spawnPoint.position,
                spawnPoint.rotation,
                room.transform);
        }
    }
}

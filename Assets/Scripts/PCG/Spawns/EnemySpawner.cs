using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject rangedEnemyPrefab;

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

            Instantiate(
                rangedEnemyPrefab,
                spawnPoint.position,
                spawnPoint.rotation,
                room.transform);
        }
    }
}

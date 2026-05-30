using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefabs;

    public void SpawnEnemies(RoomData room)
    {
        if (room == null ||
            room.enemySpawnPoints == null ||
            room.enemySpawnPoints.Length == 0)
            return;

        if (enemyPrefabs == null ||
            enemyPrefabs.Length == 0)
            return;

        foreach (Transform spawnPoint in room.enemySpawnPoints)
        {
            if (spawnPoint == null)
                continue;

            GameObject randomEnemy =
                enemyPrefabs[
                    Random.Range(0, enemyPrefabs.Length)];

            Instantiate(
                randomEnemy,
                spawnPoint.position,
                spawnPoint.rotation,
                room.transform);
        }
    }
}

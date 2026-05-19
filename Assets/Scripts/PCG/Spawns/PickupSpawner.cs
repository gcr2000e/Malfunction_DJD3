using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [Header("Pickup Prefabs")]
    [SerializeField] private GameObject medkitPrefab;
    [SerializeField] private GameObject pdaPrefab;

    [Header("PDA Data")]
    [SerializeField] private PDAData[] availablePDAs;

    [Header("Spawn Chances")]
    [SerializeField] private float medkitChance = 0.4f;
    [SerializeField] private float pdaChance = 0.3f;

    public void SpawnPickups(RoomData room)
    {
        if (room == null)
            return;

        SpawnMedkit(room);
        SpawnPDA(room);
    }

    private void SpawnMedkit(RoomData room)
    {
        if (room.medkitSpawnPoints == null ||
            room.medkitSpawnPoints.Length == 0)
            return;

        if (Random.value > medkitChance)
            return;

        Transform spawnPoint =
            room.medkitSpawnPoints[
                Random.Range(0, room.medkitSpawnPoints.Length)];

        Instantiate(
            medkitPrefab,
            spawnPoint.position,
            spawnPoint.rotation,
            room.transform);
    }

    private void SpawnPDA(RoomData room)
    {
        if (room.roomType != RoomType.Lore)
            return;

        if (room.pdaSpawnPoints == null ||
            room.pdaSpawnPoints.Length == 0)
            return;

        if (Random.value > pdaChance)
            return;

        Transform spawnPoint =
            room.pdaSpawnPoints[
                Random.Range(0, room.pdaSpawnPoints.Length)];

        GameObject spawnedPDA = Instantiate(
            pdaPrefab,
            spawnPoint.position,
            spawnPoint.rotation,
            room.transform);

        PDAPickup pdaPickup =
            spawnedPDA.GetComponent<PDAPickup>();

        if (pdaPickup != null &&
            availablePDAs.Length > 0)
        {
            PDAData randomData =
                availablePDAs[
                    Random.Range(0, availablePDAs.Length)];

            pdaPickup.SetPDAData(randomData);
        }
    }
}

using UnityEngine;

public class RoomData : MonoBehaviour
{
    [Header("Room Info")]
    public RoomType roomType;
    public SectorType sectorType;

    [Header("Doors")]
    public DoorPoint[] doors;

    [Header("Spawn Points")]
    public Transform playerSpawnPoint;

    public Transform[] enemySpawnPoints;

    public bool HasDoor(DoorDirection direction)
    {
        if (doors == null)
            return false;

        foreach (DoorPoint door in doors)
        {
            if (door != null &&
                door.direction == direction)
            {
                return true;
            }
        }

        return false;
    }
}
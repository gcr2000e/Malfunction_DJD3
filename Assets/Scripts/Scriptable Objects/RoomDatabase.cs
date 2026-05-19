using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomDatabase", menuName = "Procedural/RoomDatabase")]
public class RoomDatabase : ScriptableObject
{
    [System.Serializable]
    public class RoomEntry
    {
        public SectorType sectorType;
        public RoomType roomType;
        public RoomData[] rooms;
    }

    [SerializeField]
    private List<RoomEntry> roomEntries = new();

    public RoomData GetRandomRoom(
        SectorType sector,
        RoomType type,
        DoorDirection requiredDoor)
    {
        List<RoomData> validRooms = new();

        foreach (RoomEntry entry in roomEntries)
        {
            if (entry.sectorType != sector ||
                entry.roomType != type)
                continue;

            foreach (RoomData room in entry.rooms)
            {
                if (room != null &&
                    room.HasDoor(requiredDoor))
                {
                    validRooms.Add(room);
                }
            }
        }

        if (validRooms.Count == 0)
            return null;

        return validRooms[
            Random.Range(0, validRooms.Count)];
    }
}

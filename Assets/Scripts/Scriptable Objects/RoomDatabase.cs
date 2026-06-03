using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "RoomDatabase",
    menuName = "Procedural/Room Database")]
public class RoomDatabase : ScriptableObject
{
    public RoomData[] roomPrefabs;

    public RoomData GetRandomRoom()
    {
        if (roomPrefabs == null ||
            roomPrefabs.Length == 0)
            return null;

        return roomPrefabs[
            Random.Range(0, roomPrefabs.Length)];
    }
}

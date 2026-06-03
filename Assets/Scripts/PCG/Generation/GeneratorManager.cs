using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    [Header("Database")]
    [SerializeField]
    private RoomDatabase roomDatabase;

    [Header("Generation")]
    [SerializeField]
    private int roomCount = 15;

    private readonly List<RoomData> spawnedRooms =
        new();

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        RoomData startRoom =
            Instantiate(
                roomDatabase.GetRandomRoom(),
                Vector3.zero,
                Quaternion.identity,
                transform);

        spawnedRooms.Add(startRoom);

        for (int i = 1; i < roomCount; i++)
        {
            TrySpawnRoom();
        }
    }

    private void TrySpawnRoom()
    {
        RoomData existingRoom =
            spawnedRooms[
                Random.Range(0, spawnedRooms.Count)];

        DoorPoint existingDoor =
            existingRoom.GetRandomFreeDoor();

        if (existingDoor == null)
            return;

        RoomData roomPrefab =
            roomDatabase.GetRandomRoom();

        if (roomPrefab == null)
            return;

        RoomData newRoom =
            Instantiate(
                roomPrefab,
                Vector3.zero,
                Quaternion.identity,
                transform);

        DoorPoint newDoor =
            newRoom.GetRandomFreeDoor();

        if (newDoor == null)
        {
            Destroy(newRoom.gameObject);
            return;
        }

        AlignRoom(
            existingDoor,
            newRoom,
            newDoor);

        existingDoor.connected = true;
        newDoor.connected = true;

        spawnedRooms.Add(newRoom);
    }

    private void AlignRoom(
        DoorPoint targetDoor,
        RoomData room,
        DoorPoint roomDoor)
    {
        float angle =
            Vector3.SignedAngle(
                roomDoor.transform.forward,
                -targetDoor.transform.forward,
                Vector3.up);

        room.transform.Rotate(
            0f,
            angle,
            0f);

        Vector3 offset =
            targetDoor.transform.position -
            roomDoor.transform.position;

        room.transform.position += offset;
    }
}
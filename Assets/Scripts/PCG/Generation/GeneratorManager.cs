using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    [Header("Database")]
    [SerializeField]
    private RoomDatabase roomDatabase;

    [Header("Generation Settings")]
    [SerializeField]
    private int totalRooms = 15;

    [SerializeField]
    private float gridSize = 20f;

    [Header("References")]
    [SerializeField]
    private PlayerSpawner playerSpawner;

    [SerializeField]
    private EnemySpawner enemySpawner;

    private Dictionary<Vector2Int, RoomData> placedRooms =
        new();

    private List<Vector2Int> occupiedPositions =
        new();

    private static readonly Vector2Int[] Directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    private void Start()
    {
        GenerateDungeon();
    }

    private void GenerateDungeon()
    {
        Vector2Int startPos = Vector2Int.zero;

        RoomData startPrefab =
            roomDatabase.GetRandomRoom(
                SectorType.PerimeterSecurity,
                RoomType.Start,
                DoorDirection.North);

        RoomData startRoom = Instantiate(
            startPrefab,
            Vector3.zero,
            Quaternion.identity,
            transform);

        placedRooms.Add(startPos, startRoom);
        occupiedPositions.Add(startPos);

        List<Vector2Int> frontier =
            new() { startPos };

        while (placedRooms.Count < totalRooms &&
               frontier.Count > 0)
        {
            Vector2Int currentPos =
                frontier[Random.Range(0, frontier.Count)];

            List<(Vector2Int gridPos,
                  DoorDirection requiredDoor)>
                neighbors =
                    GetAvailableNeighbors(currentPos);

            if (neighbors.Count == 0)
            {
                frontier.Remove(currentPos);
                continue;
            }

            var chosen =
                neighbors[
                    Random.Range(0, neighbors.Count)];

            int roomIndex = placedRooms.Count;

            SectorType sector =
                GetSectorForIndex(roomIndex);

            RoomType roomType =
                GetRoomTypeForIndex(roomIndex);

            RoomData roomPrefab =
                roomDatabase.GetRandomRoom(
                    sector,
                    roomType,
                    chosen.requiredDoor);

            if (roomPrefab == null)
                continue;

            Vector3 worldPos = new(
                chosen.gridPos.x * gridSize,
                0f,
                chosen.gridPos.y * gridSize);

            RoomData newRoom = Instantiate(
                roomPrefab,
                worldPos,
                Quaternion.identity,
                transform);

            placedRooms.Add(
                chosen.gridPos,
                newRoom);

            occupiedPositions.Add(
                chosen.gridPos);

            frontier.Add(chosen.gridPos);

            SpawnRoomContent(newRoom);
        }

        SpawnPlayer();
    }

    private List<(Vector2Int, DoorDirection)>
        GetAvailableNeighbors(Vector2Int origin)
    {
        List<(Vector2Int, DoorDirection)> results =
            new();

        foreach (Vector2Int dir in Directions)
        {
            Vector2Int target = origin + dir;

            if (occupiedPositions.Contains(target))
                continue;

            DoorDirection requiredDoor =
                GetOppositeDirection(
                    VectorToDoorDirection(dir));

            results.Add((target, requiredDoor));
        }

        return results;
    }

    private void SpawnRoomContent(RoomData room)
    {
        if (room.roomType == RoomType.Combat)
        {
            enemySpawner?.SpawnEnemies(room);
        }
    }

    private void SpawnPlayer()
    {
        RoomData startRoom =
            placedRooms[Vector2Int.zero];

        if (startRoom.playerSpawnPoint != null)
        {
            playerSpawner.SpawnPlayer(
                startRoom.playerSpawnPoint);
        }
    }

    private SectorType GetSectorForIndex(int roomIndex)
    {
        float progress =
            (float)roomIndex /
            Mathf.Max(1, totalRooms - 1);

        if (progress < 0.25f)
            return SectorType.PerimeterSecurity;

        if (progress < 0.50f)
            return SectorType.DevelopmentLabs;

        if (progress < 0.75f)
            return SectorType.ProductionCore;

        return SectorType.CentralIntelligence;
    }

    private RoomType GetRoomTypeForIndex(int roomIndex)
    {
        if (roomIndex == totalRooms - 1)
            return RoomType.Final;

        float r = Random.value;

        if (r < 0.7f)
            return RoomType.Combat;

        return RoomType.Empty;
    }

    private DoorDirection VectorToDoorDirection(
        Vector2Int dir)
    {
        if (dir == Vector2Int.up)
            return DoorDirection.North;

        if (dir == Vector2Int.down)
            return DoorDirection.South;

        if (dir == Vector2Int.left)
            return DoorDirection.West;

        return DoorDirection.East;
    }

    private DoorDirection GetOppositeDirection(
        DoorDirection direction)
    {
        return direction switch
        {
            DoorDirection.North =>
                DoorDirection.South,

            DoorDirection.South =>
                DoorDirection.North,

            DoorDirection.East =>
                DoorDirection.West,

            DoorDirection.West =>
                DoorDirection.East,

            _ => DoorDirection.North
        };
    }
}
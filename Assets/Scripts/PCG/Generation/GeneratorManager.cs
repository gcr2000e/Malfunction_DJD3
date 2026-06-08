using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    [Header("Database")]
    [SerializeField]
    private RoomDatabase roomDatabase;

    [Header("Seed")]
    [SerializeField]
    private bool randomizeSeed = true;
    [SerializeField]
    private int seed = 0;

    [Header("Generation")]
    [SerializeField]
    private int roomCount = 15;
    [Tooltip("Máximo de tentativas por sala antes de desistir")]
    [SerializeField]
    private int maxAttemptsPerRoom = 8;

    private readonly List<RoomData> spawnedRooms =
        new();

    // Overload que usa seed do inspector (ou aleatória).
    public void Generate()
    {
        if (randomizeSeed)
            seed = System.Environment.TickCount;

        Generate(seed);
    }

    // Método público que recebe seed para geração determinística.
    public void Generate(int seed)
    {
        this.seed = seed;
        Random.InitState(seed);

        spawnedRooms.Clear();

        // Coloca a sala inicial (primeiro elemento) com retry.
        RoomData startPrefab = roomDatabase.GetStartRoom();
        if (startPrefab == null)
        {
            Debug.LogWarning("RoomDatabase sem salas definidas.");
            return;
        }



        RoomData startRoom = Instantiate(
            startPrefab,
            Vector3.zero,
            Quaternion.identity,
            transform);


        spawnedRooms.Add(startRoom);

        // Gera as salas intermediárias e tenta garantir a última sala ser colocada e conectada.
        for (int i = 1; i < roomCount; i++)
        {
            bool isLast = (i == roomCount - 1);
            RoomData forcedPrefab = isLast ? roomDatabase.GetEndRoom() : null;

            bool placed = false;
            int attempts = 0;
            while (!placed && attempts < maxAttemptsPerRoom)
            {
                attempts++;
                // Para sala final, usamos tentativa especial para garantir conexão.
                if (isLast && forcedPrefab != null)
                    placed = TryPlaceFinalRoom(forcedPrefab);
                else
                    placed = TrySpawnRoom(forcedPrefab);
            }

            if (!placed)
            {
                Debug.LogWarning($"Falha ao posicionar a sala #{i} (isLast={isLast}) após {maxAttemptsPerRoom} tentativas.");
                break;
            }
        }

        Debug.Log($"Geração concluída (seed={seed}). Salas geradas: {spawnedRooms.Count}");
    }

    // Tenta spawnar uma sala (forçada ou aleatória). Retorna true se conectou com sucesso.
    private bool TrySpawnRoom(RoomData forcedPrefab = null)
    {
        if (spawnedRooms.Count == 0)
            return false;

        RoomData existingRoom =
            spawnedRooms[
                Random.Range(0, spawnedRooms.Count)];

        DoorPoint existingDoor =
            existingRoom.GetRandomFreeDoor();

        if (existingDoor == null)
            return false;

        RoomData roomPrefab =
            forcedPrefab ?? roomDatabase.GetRandomRoom();

        if (roomPrefab == null)
            return false;

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
            return false;
        }

        AlignRoom(
            existingDoor,
            newRoom,
            newDoor);

        if (RoomOverlaps(newRoom))
        {
            Destroy(newRoom.gameObject);
            return false;
        }

        existingDoor.connected = true;
        newDoor.connected = true;

        spawnedRooms.Add(newRoom);
        return true;
    }

    // Tentativa mais robusta para garantir que a sala final (forçada) seja conectada.
    // Percorre tentativas buscando portas livres em salas existentes até conectar.
    private bool TryPlaceFinalRoom(RoomData finalPrefab)
    {
        if (finalPrefab == null || spawnedRooms.Count == 0)
            return false;

        // Se o prefab final não tem portas (mesmo depois de instanciar), não é possível conectar.
        // Repetimos várias tentativas tentando diferentes portas/posições.
        int attempts = 0;
        while (attempts < maxAttemptsPerRoom)
        {
            attempts++;

            // Escolhe uma sala existente aleatoriamente
            RoomData existingRoom =
                spawnedRooms[Random.Range(0, spawnedRooms.Count)];

            DoorPoint existingDoor = existingRoom.GetRandomFreeDoor();
            if (existingDoor == null)
                continue;

            RoomData newRoom = Instantiate(
                finalPrefab,
                Vector3.zero,
                Quaternion.identity,
                transform);

            DoorPoint newDoor = newRoom.GetRandomFreeDoor();
            if (newDoor == null)
            {
                Destroy(newRoom.gameObject);
                continue;
            }

            AlignRoom(existingDoor, newRoom, newDoor);

            if (RoomOverlaps(newRoom))
            {
                Destroy(newRoom.gameObject);
                continue;
            }

            // Marca como conectadas
            existingDoor.connected = true;
            newDoor.connected = true;

            spawnedRooms.Add(newRoom);
            return true;
        }

        return false;
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

    private bool RoomOverlaps(RoomData room)
    {
        if (room.roomBounds == null)
            return false;

        Bounds bounds = room.roomBounds.bounds;

        foreach (RoomData other in spawnedRooms)
        {
            if(other == room)
                continue;

            if (other.roomBounds == null)
                continue;

            if (bounds.Intersects(other.roomBounds.bounds))
            {
                Debug.Log($"Overlap entre {room.name} e {other.name}");
                return true;
            }
        }

        return false;
    }
}
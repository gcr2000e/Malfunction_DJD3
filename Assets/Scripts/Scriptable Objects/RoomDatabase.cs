using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "RoomDatabase",
    menuName = "Procedural/Room Database")]
public class RoomDatabase : ScriptableObject
{
    public RoomData[] roomPrefabs;

    // Retorna a sala inicial (primeiro elemento) ou null se não existir.
    public RoomData GetStartRoom()
    {
        if (roomPrefabs == null || roomPrefabs.Length == 0)
            return null;

        return roomPrefabs[0];
    }

    // Retorna a sala final (último elemento) ou null se não existir.
    public RoomData GetEndRoom()
    {
        if (roomPrefabs == null || roomPrefabs.Length == 0)
            return null;

        return roomPrefabs[roomPrefabs.Length - 1];
    }

    // Retorna uma sala aleatória excluindo a primeira e a última quando houver
    // pelo menos 3 entradas (mantém start/end fixas).
    public RoomData GetRandomRoom()
    {
        if (roomPrefabs == null || roomPrefabs.Length == 0)
            return null;

        if (roomPrefabs.Length <= 2)
            return roomPrefabs[Random.Range(0, roomPrefabs.Length)];

        int index = Random.Range(1, roomPrefabs.Length - 1);
        return roomPrefabs[index];
    }
}

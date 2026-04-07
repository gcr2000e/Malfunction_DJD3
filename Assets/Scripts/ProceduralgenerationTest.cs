using System.Collections.Generic;
using UnityEngine;

public class ProceduralgenerationTest : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    public int roomCount = 8;
    public float roomSpacing = 12f;

    private List<GameObject> spawnedRooms = new List<GameObject>();

    void Start()
    {
        Generate();
    }

    void Update()
    {
        // Pressiona R para regenerar
        if (Input.GetKeyDown(KeyCode.R))
        {
            ClearDungeon();
            Generate();
        }
    }

    void Generate()
    {
        Vector3 spawnPos = Vector3.zero;

        for (int i = 0; i < roomCount; i++)
        {
            GameObject room = Instantiate(
                roomPrefabs[Random.Range(0, roomPrefabs.Length)],
                spawnPos,
                Quaternion.identity
            );

            spawnedRooms.Add(room);

            // Próxima posição (linha simples)
            spawnPos += new Vector3(0, 0, roomSpacing);
        }
    }

    void ClearDungeon()
    {
        foreach (GameObject room in spawnedRooms)
        {
            Destroy(room);
        }

        spawnedRooms.Clear();
    }
}

using UnityEngine;

public class RoomData : MonoBehaviour
{
    [Header("Doors")]
    public DoorPoint[] doors;

    [Header("Bounds")]
    public BoxCollider roomBounds;

    public DoorPoint GetRandomFreeDoor()
    {
        if (doors == null)
            return null;

        System.Collections.Generic.List<DoorPoint> freeDoors =
            new();

        foreach (DoorPoint door in doors)
        {
            if (door == null)
                continue;

            if (!door.connected)
                freeDoors.Add(door);
        }

        if (freeDoors.Count == 0)
            return null;

        return freeDoors[
            Random.Range(0, freeDoors.Count)];
    }
}
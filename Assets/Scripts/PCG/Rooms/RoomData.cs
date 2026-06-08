using UnityEngine;

public class RoomData : MonoBehaviour
{
    public DoorPoint[] doors;
    public BoxCollider roomBounds;

    public DoorPoint GetRandomFreeDoor()
    {
        System.Collections.Generic.List<DoorPoint> freeDoors =
            new();

        foreach (DoorPoint door in doors)
        {
            if (!door.connected)
                freeDoors.Add(door);
        }

        if (freeDoors.Count == 0)
            return null;

        return freeDoors[
            Random.Range(0, freeDoors.Count)];
    }
}
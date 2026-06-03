using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [SerializeField]
    private bool startOpen;

    private void Start()
    {
        if (startOpen)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void Open()
    {

    }

    public void Close()
    {

    }
}

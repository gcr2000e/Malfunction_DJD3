using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private void Update()
    {
        transform.position = target.position;
    }
}

using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private void Start()
    {
        target.rotation = transform.rotation;
    }

    private void Update()
    {
        transform.position = target.position;
    }
}

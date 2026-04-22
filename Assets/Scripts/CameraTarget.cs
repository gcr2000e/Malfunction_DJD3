using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private LayerMask cameraRaycastIgnoreLayers;

    private Camera cam;

    private void Start()
    {
        target.rotation = transform.rotation;
        cam = FindFirstObjectByType<Camera>();
    }

    private void Update()
    {
        transform.position = target.position;
    }

    public Vector3 getMouseWorldPosition()
    {
        // Get the mouse position
        Vector2 mousePos = 
            InputSystem.actions["MousePos"]
            .ReadValue<Vector2>();

        // Convert to world with raycast
        Ray ray = cam.ScreenPointToRay(mousePos);
        Physics.Raycast(
            ray, 
            out RaycastHit hit, 
            Mathf.Infinity, 
            cameraRaycastIgnoreLayers);

        // Debug
        Debug.DrawLine(ray.origin, ray.GetPoint(10));

        return hit.point;
    }
}

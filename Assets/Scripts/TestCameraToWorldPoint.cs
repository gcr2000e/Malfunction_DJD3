using UnityEngine;
using UnityEngine.InputSystem;

public class TestCameraToWorldPoint : MonoBehaviour
{
    [SerializeField] LayerMask detectionLayerMask;

    public bool debug = true;
    private Camera mainCam;

    private void Start()
    {
        mainCam = FindFirstObjectByType<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = InputSystem.actions["MousePos"].ReadValue<Vector2>();
        Ray ray = mainCam.ScreenPointToRay(mousePos);

        if (debug)
            Debug.DrawLine(ray.origin, ray.GetPoint(10));

        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, detectionLayerMask);

        transform.position = hit.point;

    }
}

using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField]
    private bool canRotate = true;

    private CameraTarget cam;

    private void Start()
    {
        cam = FindFirstObjectByType<CameraTarget>();
    }

    private void Update()
    {
        if (canRotate)
            ModelRotation();
    }

    private void ModelRotation()
    {
        // Get rotation from mouse
        Vector3 mousePos = cam.getMouseWorldPosition();
        // Make it so character only rotates on the z axis
        mousePos.y = transform.position.y;

        // Apply rotation to model
        transform.LookAt(mousePos);
    }
}

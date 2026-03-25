using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Rigidbody rb;

    private bool canMove = false;

    private Vector3 moveDir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            throw new MissingReferenceException("No Rigidbody in Player");
        else canMove = true;

        // Make sure starting speed is zero
        moveDir = Vector3.zero;
    }
    private void Update()
    {
        if (canMove)
        {
            // Set input directions to move direction 
            moveDir.x = InputSystem.actions["Move"].ReadValue<Vector2>().x;
            moveDir.z = InputSystem.actions["Move"].ReadValue<Vector2>().y;

            rb.linearVelocity = 
                moveDir.normalized * moveSpeed;
        }
    }
}



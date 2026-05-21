using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private bool canMove;

    [Header("Running")]
    [SerializeField]
    private float moveSpeed;

    [Header("Dodge")]
    [SerializeField]
    private float dodgeStrenght;
    [SerializeField]
    private float dodgeCooldown;
    private float dodgeCooldownTimer;
    private IEnumerator dodgeCoroutine;

    private Animator animator;
    private CameraTarget cam;
    private Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cam = FindFirstObjectByType<CameraTarget>();
        rb = GetComponent<Rigidbody>();
        cam = FindFirstObjectByType<CameraTarget>();
        if (rb == null)
            throw new MissingReferenceException("No Rigidbody in Player");
        else canMove = true;
    }

    private void Update()
    {
        Vector3 movementDir = MoveInputDir();

        // Transform movement dir to local
        Vector3 localMoveDir = transform.InverseTransformDirection(movementDir);

        //Send movement direction to animator
        animator.SetFloat("VerticalInput", localMoveDir.z);
        animator.SetFloat("HorizontalInput", localMoveDir.x);

        // First priority is dodge input
        if (InputSystem.actions["Dodge"].WasPressedThisFrame()
            && dodgeCooldownTimer + dodgeCooldown < Time.time)
            Dodge();

        else if (canMove)
            // If possible do movement
            rb.linearVelocity = movementDir * moveSpeed;
    }

    private Vector3 MoveInputDir()
    {
        return (cam.transform.right *
            InputSystem.actions["Move"].ReadValue<Vector2>().x
                + cam.transform.forward *
            InputSystem.actions["Move"].ReadValue<Vector2>().y)
                .normalized;
    }

    private void Dodge()
    {
        // Set cooldown timer
        dodgeCooldownTimer = Time.time;

        // Reset attack triggers
        animator.ResetTrigger("Attack");
        // Set animation trigger
        animator.SetTrigger("Dodge");


        // Dodge in input direction
        Vector3 movementDir = MoveInputDir();

        // If no direction is being pressed default to forward
        if (movementDir == Vector3.zero)
            movementDir = transform.forward;

        // The actual dodge
        rb.AddForce(movementDir * dodgeStrenght);
    }
}

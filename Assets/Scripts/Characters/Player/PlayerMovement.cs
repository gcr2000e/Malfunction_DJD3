using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("AnimationControl")]
    [SerializeField]
    private bool canMove;
    [SerializeField]
    private bool canDodge;
    [SerializeField]
    private float animationMovement;

    [Header("Running")]
    [SerializeField]
    private float moveSpeed;

    [Header("Dodge")]
    [SerializeField]
    private float dodgeCooldown;
    private float dodgeCooldownTimer;

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
            && canDodge
            && dodgeCooldownTimer + dodgeCooldown < Time.time)
            Dodge();

        else if (canMove)
            // If possible do movement
            rb.linearVelocity = movementDir * moveSpeed;
        else
            // Do animation based movement
            rb.linearVelocity = transform.forward * animationMovement;
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
    }
}

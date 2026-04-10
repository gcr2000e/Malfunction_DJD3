using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField]
    private float moveSpeed;

    [Header ("Dodge")]
    [SerializeField]
    private float dodgeStrenght;
    [SerializeField]
    private float dodgeCooldown;
    private float dodgeCooldownTimer;

    private Rigidbody rb;

    private bool canMove = false;

    private bool queuedAttack = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            throw new MissingReferenceException("No Rigidbody in Player");
        else canMove = true;
    }
    private void Update()
    {
        // First priority is dodge input
        if (InputSystem.actions["Dodge"].WasPressedThisFrame()
            && dodgeCooldownTimer + dodgeCooldown < Time.time)
            Dodge();

        // Then check for attack input or queued attack
        if (InputSystem.actions["Attack"].WasPressedThisFrame() ||
            queuedAttack)
        {
            if (canMove) Attack();
            else queuedAttack = true;
        }

        if (canMove)
        {
            // If none of the previous inputs were chosen move
            rb.linearVelocity = MoveInputDir() * moveSpeed;
        }
    }

    private Vector3 MoveInputDir()
    {
        return (transform.right * 
            InputSystem.actions["Move"].ReadValue<Vector2>().x
                + transform.forward * 
            InputSystem.actions["Move"].ReadValue<Vector2>().y)
                .normalized;
    }

    private void Dodge()
    {
        // Set cooldown timer
        dodgeCooldownTimer = Time.time;

        // Dodge in input direction
        Vector3 movementDir = MoveInputDir();

        // If no direction is being pressed default to forward
        if (movementDir == Vector3.zero)
            movementDir = transform.forward;

        // The actual dodge
        rb.AddForce(movementDir * dodgeStrenght);

        // StartCoroutine for the invincibility frames

        // Allow player to move
        canMove = true;
        // Dequeue attacks
        queuedAttack = false;

        // Stop attack coroutines
    }

    private void Attack()
    {
        // Turn on hitbox that is turned off at the end of coroutine
        
        // Start coroutine that makes you unable to move for the duration
    }
}



using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField]
    private float moveSpeed;

    [Header("Combat")]
    [SerializeField]
    private GameObject hitbox;
    [SerializeField]
    private float attackTime;
    [SerializeField]
    private float attackCooldown;
    private IEnumerator attackCoroutine;

    // Count the hit of the combo
    private uint attackCount = 0;

    private bool canAttack = true;

    [Header ("Dodge")]
    [SerializeField]
    private float dodgeStrenght;
    [SerializeField]
    private float dodgeCooldown;
    private float dodgeCooldownTimer;
    private IEnumerator dodgeCoroutine;

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
        if (canAttack && 
            (InputSystem.actions["Attack"].WasPressedThisFrame() ||
            queuedAttack))
        {
            if (canMove)
            {
                attackCoroutine = Attack();
                StartCoroutine(attackCoroutine);
            }
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
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
    }

    private IEnumerator Attack()
    {
        // Turn on hitbox
        hitbox.SetActive(true);

        // Wait for duration
        yield return new WaitForSeconds(attackTime);

        // Turn off hitbox
        hitbox.SetActive(false);

        //Enable movement again
        canMove = true;

        // Last hit of combo
        if (attackCount >= 3)
        {
            // Disable attacking
            canAttack = false;
            // Reset combo
            attackCount = 1;
        }
        // Otherwise
        else
        {
            // Update hit in combo
            attackCount++;
            StopCoroutine(attackCoroutine);
        }

        // Wait cooldown time
        yield return new WaitForSeconds(attackCooldown);

        // Enable attacking
        canAttack = true;
    }
}



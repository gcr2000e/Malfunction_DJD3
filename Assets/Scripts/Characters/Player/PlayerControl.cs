using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    private CameraTarget cam;

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

    private bool canMove = false;

    private bool queuedAttack = false;

    private void Update()
    {
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



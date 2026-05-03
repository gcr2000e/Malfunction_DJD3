using UnityEngine;

public abstract class IEnemy : MonoBehaviour
{
    [Header("Movement")]
    private Transform target;

    [SerializeField]
    protected float moveSpeed;

    [SerializeField]
    private bool canMove = true;

    protected Rigidbody rb;

    [Header("Combat")]
    [SerializeField]
    protected uint attackStrenght;
    [SerializeField]
    protected float attackDistance;

    [Header("Visual")]
    [SerializeField]
    protected GameObject model;
    [SerializeField]
    private bool canRotate = true;

    protected Animator animator;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        // Get player as target when spawned
        target = FindFirstObjectByType<PlayerControl>()
            .gameObject
            .transform;
    }
    private void Update()
    {
        if (!TargetBlocked())
        {
            if (canRotate)
            {
                ModelRotation();
            }
            if (canMove)
            {
                if (InRange())
                    Attack();
                else
                    DoMovement();
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
    }

    private void ModelRotation()
    {
        Vector3 lookAtPos = target.position;
        lookAtPos.y = model.transform.position.y;

        // Apply rotation to model
        model.transform.LookAt(lookAtPos);
    }

    private bool InRange()
    {
        // Get the distance between the two
        float distance =
            (target.position - transform.position)
            .magnitude;
        // If the distance is less or equal
        return (distance <= attackDistance);
    }

    public void Stun()
    {
        // Don't stun an already stunned enemy
        if (canMove)
        {
            animator.SetTrigger("Stun");
        }
    }

    private bool TargetBlocked()
    {
        Vector3 rayDir = target.position - transform.position;

        RaycastHit hit;

        if (Physics.Raycast(
            // Origin
            transform.position,
            // Direction
            rayDir.normalized,
            // Hit
            out hit,
            // Max Distance
            rayDir.magnitude))
        {
            // if it's the player it's not blocked
            return hit.transform != target;
        }
        else return true;
    }

    protected abstract void Attack();
    protected abstract void DoMovement();
}

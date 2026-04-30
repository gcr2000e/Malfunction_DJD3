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
        if (canRotate)
        {
            ModelRotation();
        }
        if (InRange())
        {
            Attack();
        }
        else if (canMove)
        {
            DoMovement();
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

    protected abstract void Attack();
    protected abstract void DoMovement();
}

using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Movement")]
    private Transform target;
    
    [SerializeField]
    private float moveSpeed;
    
    [SerializeField]
    private bool canMove = true;
    
    private Rigidbody rb;

    [Header("Combat")]
    [SerializeField]
    private uint attackStrenght;
    [SerializeField]
    private float attackDistance;

    [Header("Visual")]
    [SerializeField]
    private GameObject model;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Get player as target when spawned
        target = FindFirstObjectByType<PlayerControl>()
            .gameObject
            .transform;
    }

    private void Update()
    {
        if (InRange())
        {
            // Do attack anim
        }
        else if (canMove)
        {
            MoveToTarget();
            // Do move anim
        }
    }

    private void MoveToTarget()
    {
        // Rotate model
        ModelRotation();
        // Move in desired direction
        rb.linearVelocity = model.transform.forward * moveSpeed;
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
}

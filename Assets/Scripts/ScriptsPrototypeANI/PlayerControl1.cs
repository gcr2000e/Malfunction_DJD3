using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl1 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject model;

    private CameraTarget cam;

    [Header("Combat")]
    [SerializeField] private GameObject hitbox;
    [SerializeField] private float attackTime;
    [SerializeField] private float attackCooldown;
    private IEnumerator attackCoroutine;
    private uint attackCount = 0;
    private bool canAttack = true;

    [Header("Dodge")]
    [SerializeField] private float dodgeStrenght;
    [SerializeField] private float dodgeCooldown;
    private float dodgeCooldownTimer;

    // Velocidade atual do dodge (vai decaindo até zero)
    private Vector3 dodgeVelocity = Vector3.zero;
    [Tooltip("O quão rápido a velocidade do dodge decai (maior = dodge mais curto)")]
    [SerializeField] private float dodgeDecay = 8f;

    private readonly HashSet<Bullet2> bulletsInWindow = new();

    private Rigidbody rb;
    private bool canMove = false;
    private bool queuedAttack = false;

    public void OpenPerfectDodgeWindow(Bullet2 bullet)  => bulletsInWindow.Add(bullet);
    public void ClosePerfectDodgeWindow(Bullet2 bullet) => bulletsInWindow.Remove(bullet);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = FindFirstObjectByType<CameraTarget>();

        if (rb == null)
            throw new MissingReferenceException("No Rigidbody in Player");

        // O Rigidbody existe apenas para colisões —
        // o movimento é feito via Transform para ficar imune ao timeScale
        rb.isKinematic = true;

        canMove = true;
    }

    private void Update()
    {
        ModelRotation();

        if (InputSystem.actions["Dodge"].WasPressedThisFrame()
            && dodgeCooldownTimer + dodgeCooldown < Time.unscaledTime)
        {
            bool isPerfect = bulletsInWindow.Count > 0;
            Dodge(isPerfect);
        }

        if (canAttack &&
            (InputSystem.actions["Attack"].WasPressedThisFrame() || queuedAttack))
        {
            if (canMove)
            {
                attackCoroutine = Attack();
                StartCoroutine(attackCoroutine);
            }
            else queuedAttack = true;
        }

        if (canMove)
            MovePlayer();
    }

    private void MovePlayer()
    {
        // Movimento normal — usa unscaledDeltaTime para ignorar o timeScale
        Vector3 moveVelocity = MoveInputDir() * moveSpeed;

        // Combina com o dodge que vai decaindo
        dodgeVelocity = Vector3.Lerp(
            dodgeVelocity, Vector3.zero, dodgeDecay * Time.unscaledDeltaTime);

        Vector3 finalVelocity = moveVelocity + dodgeVelocity;

        transform.position += finalVelocity * Time.unscaledDeltaTime;
    }

    private Vector3 MoveInputDir()
    {
        return (transform.right  * InputSystem.actions["Move"].ReadValue<Vector2>().x
              + transform.forward * InputSystem.actions["Move"].ReadValue<Vector2>().y)
              .normalized;
    }

    private void ModelRotation()
    {
        Vector3 mousePos = cam.getMouseWorldPosition();
        mousePos.y = model.transform.position.y;
        model.transform.LookAt(mousePos);
    }

    private void Dodge(bool perfectDodge = false)
    {
        dodgeCooldownTimer = Time.unscaledTime;

        Vector3 movementDir = MoveInputDir();
        if (movementDir == Vector3.zero)
            movementDir = transform.forward;

        // Guarda a velocidade do dodge — vai ser consumida no MovePlayer()
        dodgeVelocity = movementDir * dodgeStrenght;

        canMove = true;
        queuedAttack = false;
        bulletsInWindow.Clear();

        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        if (perfectDodge && SlowMotionManager.Instance != null)
            SlowMotionManager.Instance.TriggerSlowMotion();
    }

    private IEnumerator Attack()
    {
        hitbox.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        hitbox.SetActive(false);

        canMove = true;

        if (attackCount >= 3)
        {
            canAttack = false;
            attackCount = 1;
        }
        else
        {
            attackCount++;
            StopCoroutine(attackCoroutine);
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}

using UnityEngine;

public class Bullet2 : Damage
{
    [SerializeField] private float maxLifeTime;
    private float lifeTime;

    [Header("Speed")]
    [Tooltip("Velocidade da bala em unidades/segundo")]
    [SerializeField] private float speed = 10f;

    [Tooltip("Drag aplicado — quanto maior mais a bala abranda. 0 = velocidade constante.")]
    [SerializeField] private float bulletDrag = 0f;

    [Header("Perfect Dodge")]
    [Tooltip("Raio à volta da bala que ativa a janela de perfect dodge")]
    [SerializeField] private float perfectDodgeRadius = 1.5f;

    private PlayerControl1 playerControl;
    private bool windowOpen = false;

    private Vector3 moveDirection;
    private float currentSpeed;

    private void Start()
    {
        playerControl = FindFirstObjectByType<PlayerControl1>();
        moveDirection = transform.forward;
        currentSpeed = speed;

        // isKinematic = true é OBRIGATÓRIO para a bala não ser afetada pela física
        // e não colidir com o chão/paredes por causa da gravidade
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        // Log para identificar o que está a destruir a bala
        Debug.Log($"[Bullet2] Destruída por colisão com: '{other.gameObject.name}' " +
                  $"(tag: {other.tag}, layer: {LayerMask.LayerToName(other.gameObject.layer)}) " +
                  $"ao fim de {lifeTime:F2}s");

        base.OnTriggerEnter(other);
        CloseWindow();
        Destroy(gameObject);
    }

    private void Update()
    {
        lifeTime += Time.unscaledDeltaTime;
        if (lifeTime >= maxLifeTime)
        {
            Debug.Log($"[Bullet2] Destruída por maxLifeTime ({maxLifeTime}s)");
            CloseWindow();
            Destroy(gameObject);
            return;
        }

        if (bulletDrag > 0f)
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, bulletDrag * Time.unscaledDeltaTime);

        float multiplier = SlowMotionManager.Instance != null
            ? SlowMotionManager.Instance.SpeedMultiplier
            : 1f;

        transform.position += moveDirection * currentSpeed * multiplier * Time.unscaledDeltaTime;

        if (playerControl == null) return;

        float dist = Vector3.Distance(transform.position, playerControl.transform.position);
        bool inRange = dist <= perfectDodgeRadius;

        if (inRange && !windowOpen)
        {
            windowOpen = true;
            playerControl.OpenPerfectDodgeWindow(this);
        }
        else if (!inRange && windowOpen)
        {
            CloseWindow();
        }
    }

    private void CloseWindow()
    {
        if (windowOpen)
        {
            windowOpen = false;
            playerControl?.ClosePerfectDodgeWindow(this);
        }
    }

    private void OnDestroy() => CloseWindow();

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.92f, 0.016f, 0.15f);
        Gizmos.DrawSphere(transform.position, perfectDodgeRadius);

        Gizmos.color = new Color(1f, 0.92f, 0.016f, 0.6f);
        Gizmos.DrawWireSphere(transform.position, perfectDodgeRadius);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.4f, 0f, 0.25f);
        Gizmos.DrawSphere(transform.position, perfectDodgeRadius);

        Gizmos.color = new Color(1f, 0.4f, 0f, 1f);
        Gizmos.DrawWireSphere(transform.position, perfectDodgeRadius);

        UnityEditor.Handles.color = Color.white;
        UnityEditor.Handles.Label(
            transform.position + Vector3.up * (perfectDodgeRadius + 0.1f),
            $"Perfect Dodge: {perfectDodgeRadius:F1}u"
        );
    }
#endif
}
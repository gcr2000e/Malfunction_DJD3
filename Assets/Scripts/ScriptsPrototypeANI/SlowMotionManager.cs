using System.Collections;
using UnityEngine;

public class SlowMotionManager : MonoBehaviour
{
    public static SlowMotionManager Instance { get; private set; }

    [Header("Slow Motion")]
    [Tooltip("Multiplicador de velocidade durante o slow-mo (0.15 = 15% da velocidade normal)")]
    [SerializeField] private float slowMultiplier = 0.15f;

    [Tooltip("Duração em segundos reais")]
    [SerializeField] private float slowDuration = 0.5f;

    [Tooltip("Velocidade de retorno ao normal")]
    [SerializeField] private float recoverySpeed = 5f;

    public float SpeedMultiplier { get; private set; } = 1f;
    public bool IsSlowMo { get; private set; } = false;

    private Coroutine activeCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void TriggerSlowMotion()
    {
        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);
        activeCoroutine = StartCoroutine(SlowMotionRoutine());
    }

    private IEnumerator SlowMotionRoutine()
    {
        // Ativa slow-mo
        IsSlowMo = true;
        SpeedMultiplier = slowMultiplier;

        // Espera slowDuration em tempo REAL
        float elapsed = 0f;
        while (elapsed < slowDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        // Volta suavemente a 1
        while (SpeedMultiplier < 1f)
        {
            SpeedMultiplier = Mathf.MoveTowards(
                SpeedMultiplier, 1f, recoverySpeed * Time.unscaledDeltaTime);
            yield return null;
        }

        SpeedMultiplier = 1f;
        IsSlowMo = false;
        activeCoroutine = null;
    }

    // Debug visual no Inspector para confirmares que o valor muda
    private void OnGUI()
    {
#if UNITY_EDITOR
        GUI.Label(new Rect(10, 10, 300, 20),
            $"SpeedMultiplier: {SpeedMultiplier:F2}  |  SlowMo: {IsSlowMo}");
#endif
    }
}
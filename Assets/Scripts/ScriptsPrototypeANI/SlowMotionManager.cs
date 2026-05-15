using System.Collections;
using UnityEngine;

/// <summary>
/// NÃO altera o Time.timeScale — usa um SpeedMultiplier que as balas
/// e inimigos lêem manualmente. O jogador ignora-o completamente.
/// </summary>
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

    // Lido pelas balas e inimigos para escalar o seu movimento
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
        IsSlowMo = true;
        SpeedMultiplier = slowMultiplier;

        float elapsed = 0f;
        while (elapsed < slowDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

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
}
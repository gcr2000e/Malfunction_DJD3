using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

/// <summary>
/// Gere toda a UI de interação.
/// Coloca este script num GameObject vazio chamado "InteractionManager".
/// </summary>
public class InteractionUI : MonoBehaviour
{
    [Header("Painel Principal da UI")]
    public GameObject painelUI;
    public TextMeshProUGUI textoTitulo;
    public TextMeshProUGUI textoConteudo;
    public Button botaoFechar;

    [Header("Prompt de Interação (Press E)")]
    public GameObject promptInteracao;
    public TextMeshProUGUI textoPrompt;

    [Header("Deteção de Proximidade")]
    public Transform jogador;
    public float intervaloVerificacao = 0.1f;

    private InteractableObject objetoAnterior = null;
    private float timerVerificacao = 0f;

    private void Start()
    {
        if (painelUI) painelUI.SetActive(false);
        if (promptInteracao) promptInteracao.SetActive(false);

        if (botaoFechar)
            botaoFechar.onClick.AddListener(FecharUI);

        if (jogador == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            if (go) jogador = go.transform;
            else Debug.LogWarning("[InteractionUI] Jogador não encontrado! Adiciona a tag 'Player' ou arrasta manualmente.");
        }
    }

    private void Update()
    {
        // Fecha com Escape
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            if (painelUI && painelUI.activeSelf) FecharUI();

        // Verifica proximidade em intervalos
        timerVerificacao -= Time.deltaTime;
        if (timerVerificacao > 0f) return;
        timerVerificacao = intervaloVerificacao;

        VerificarProximidade();
    }

    private void VerificarProximidade()
    {
        if (jogador == null) return;

        InteractableObject maisProximo = null;
        float menorDistancia = Mathf.Infinity;

        InteractableObject[] todos = FindObjectsByType<InteractableObject>(FindObjectsSortMode.None);

        foreach (var obj in todos)
        {
            float dist = Vector3.Distance(jogador.position, obj.transform.position);
            if (dist <= obj.distanciaInteracao && dist < menorDistancia)
            {
                menorDistancia = dist;
                maisProximo = obj;
            }
        }

        if (objetoAnterior != null && objetoAnterior != maisProximo)
            objetoAnterior.SairZona();

        if (maisProximo != null && maisProximo != objetoAnterior)
            maisProximo.EntrarZona();

        objetoAnterior = maisProximo;
    }

    public void AbrirUI(string titulo, string conteudo)
    {
        if (painelUI == null) return;

        if (textoTitulo) textoTitulo.text = titulo;
        if (textoConteudo) textoConteudo.text = conteudo;

        painelUI.SetActive(true);

        // Opcional: pausa o jogo enquanto a UI está aberta
        // Time.timeScale = 0f;
    }

    public void FecharUI()
    {
        if (painelUI) painelUI.SetActive(false);

        // Time.timeScale = 1f;
    }

    public void MostrarPrompt(bool mostrar, string nomeObjeto = "")
    {
        if (promptInteracao) promptInteracao.SetActive(mostrar);

        if (textoPrompt && mostrar)
            textoPrompt.text = $"Open PDA [E]";
    }
}
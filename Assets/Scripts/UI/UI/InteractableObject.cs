using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Coloca este script em qualquer objeto que queiras tornar interativo.
/// Define o título e o texto que aparecerá na UI ao interagir.
/// </summary>
public class InteractableObject : MonoBehaviour
{
    [Header("Configuração da UI")]
    [TextArea(3, 8)]
    public string titulo = "Objeto Interativo";

    [TextArea(5, 15)]
    public string textoConteudo = "Escreve aqui o texto que aparecerá quando o jogador interagir com este objeto.";

    [Header("Configuração de Proximidade")]
    public float distanciaInteracao = 3f;

    private InteractionUI interactionUI;
    private bool jogadorProximo = false;

    private void Start()
    {
        interactionUI = FindFirstObjectByType<InteractionUI>();

        if (interactionUI == null)
            Debug.LogWarning($"[{gameObject.name}] Nenhum InteractionUI encontrado na cena!");
    }

    private void Update()
    {
        if (!jogadorProximo) return;

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            Interagir();
    }

    public void EntrarZona()
    {
        jogadorProximo = true;
        interactionUI?.MostrarPrompt(true, gameObject.name);
    }

    public void SairZona()
    {
        jogadorProximo = false;
        interactionUI?.MostrarPrompt(false);
        interactionUI?.FecharUI();
    }

    private void Interagir()
    {
        interactionUI?.AbrirUI(titulo, textoConteudo);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaInteracao);
    }
}
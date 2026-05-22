using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PDAInteractionSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject interactionText;
    [SerializeField] private GameObject pdaPanel;

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text messageText;

    private PDAInteractable currentPDA;

    private bool panelOpen = false;

    private void Start()
    {
        interactionText.SetActive(false);
        pdaPanel.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (currentPDA != null)
            {
                if (!panelOpen)
                {
                    OpenPDA();
                }
                else
                {
                    ClosePDA();
                }
            }
        }
    }

    private void OpenPDA()
    {
        panelOpen = true;

        pdaPanel.SetActive(true);

        titleText.text = currentPDA.pdaTitle;
        messageText.text = currentPDA.pdaMessage;

        interactionText.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ClosePDA()
    {
        panelOpen = false;

        pdaPanel.SetActive(false);

        interactionText.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        PDAInteractable pda = other.GetComponent<PDAInteractable>();

        if (pda != null)
        {
            currentPDA = pda;

            if (!panelOpen)
                interactionText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PDAInteractable pda = other.GetComponent<PDAInteractable>();

        if (pda != null && pda == currentPDA)
        {
            currentPDA = null;

            interactionText.SetActive(false);

            if (panelOpen)
            {
                ClosePDA();
            }
        }
    }
}

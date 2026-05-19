using UnityEngine;
using TMPro;

public class PDAUIManager : MonoBehaviour
{
    public static PDAUIManager Instance;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI bodyText;

    private bool isOpen;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        panel.SetActive(false);
    }

    private void Update()
    {
        if (isOpen &&
            (Input.GetKeyDown(KeyCode.E) ||
             Input.GetKeyDown(KeyCode.Escape)))
        {
            ClosePDA();
        }
    }

    public void ShowPDA(PDAData data)
    {
        titleText.text = data.pdaTitle;
        bodyText.text = data.pdaText;

        panel.SetActive(true);

        isOpen = true;

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ClosePDA()
    {
        panel.SetActive(false);

        isOpen = false;

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
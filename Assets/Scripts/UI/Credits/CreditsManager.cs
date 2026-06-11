using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CreditsScroll : MonoBehaviour
{
    public RectTransform creditsContainer;
    public float scrollSpeed = 90f;
    public string menuSceneName = "MainMenu";
    public Button exitButton;

    [Header("Fade")]
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 1.5f;

    float endY;
    bool fading = false;

    void Start()
    {
        exitButton.gameObject.SetActive(false);
        // Force layout update to get correct height
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(creditsContainer);

        // Final Y
        endY = creditsContainer.rect.height + Screen.height;

        // No fade at start
        if (fadeCanvas != null)
            fadeCanvas.alpha = 0f;
    }

    void Update()
    {
        StartCoroutine(EnableExitButton());

        if (fading) return;

        creditsContainer.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (creditsContainer.anchoredPosition.y >= endY)
        {
            StartCoroutine(FadeAndExit());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(menuSceneName);
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    IEnumerator FadeAndExit()
    {
        fading = true;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = t / fadeDuration;
            yield return null;
        }

        fadeCanvas.alpha = 1f;
        SceneManager.LoadScene(menuSceneName);
    }

    IEnumerator EnableExitButton()
    {
        yield return new WaitForSeconds(5);
        exitButton.gameObject.SetActive(true);
    }
}
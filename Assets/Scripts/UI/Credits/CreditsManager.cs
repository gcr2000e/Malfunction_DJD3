using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        StartCoroutine(EnableExitButton());
        exitButton.gameObject.SetActive(false);
        // Force layout update to get correct height
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(creditsContainer);

        // Final Y
        endY = creditsContainer.rect.height + (Screen.height * 4f);

        // No fade at start
        if (fadeCanvas != null)
            fadeCanvas.alpha = 0f;
    }

    void Update()
    {
        if (fading) return;

        creditsContainer.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (creditsContainer.anchoredPosition.y >= endY)
        {
            StartCoroutine(FadeAndExit());
        }

        //if (InputSystem.actions["SkipCredits"].WasPressedThisDynamicUpdate())
        //{
        //    SceneManager.LoadScene(menuSceneName);
        //}
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
        yield return new WaitForSeconds(10);
        exitButton.gameObject.SetActive(true);
    }
}
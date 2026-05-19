using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Levels To Load")]
    public string _newGameLevel;

    public void StartGame()
    {
        SceneManager.LoadScene(_newGameLevel);
        Debug.Log("New Game Started");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}

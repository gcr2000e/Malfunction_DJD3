using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LoadGame : MonoBehaviour
{
    private string savePath =>
        Path.Combine(Application.persistentDataPath, "save.json");
    private string persistentDataPath =>
    Path.Combine(Application.persistentDataPath, "persistentData.json");

    public void LoadLevel()
    {
        if (File.Exists(savePath))
        {
            SaveData data =
                JsonUtility
                .FromJson<SaveData>(
                    File.ReadAllText(savePath));

            SceneManager.LoadScene(data.currentLevel);
        }
    }

    public void NewGame()
    {
        if (File.Exists(savePath))
            File.Delete(savePath);
        if (File.Exists(persistentDataPath))
            File.Delete(persistentDataPath);
        SceneManager.LoadScene("Level02");
    }
}

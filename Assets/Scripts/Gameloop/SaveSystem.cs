using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private PlayerCombat pCombat;
    private PlayerHealth pHealth;

    private EnemyManager eManager;

    private string savePath => 
        Path.Combine(Application.persistentDataPath, "save.json");
    private string persistentDataPath =>
        Path.Combine(Application.persistentDataPath, "persistentData.json");
    
    private void Start()
    {
        pCombat = FindAnyObjectByType<PlayerCombat>();
        pHealth = FindAnyObjectByType<PlayerHealth>();
        eManager = FindAnyObjectByType<EnemyManager>();

        LoadGame();
    }

    public void SaveGame()
    {
        SavePersistentData();

        SaveData saveData = new SaveData
        {
            playerPos = pCombat.transform.position,
            aliveEnemies = eManager.GetDeadEnemies(),
            currentLevel = SceneManager.GetActiveScene().name
        };

        // Write to file
        File.WriteAllText(
            savePath,
            JsonUtility.ToJson(saveData));
    }

    public void SavePersistentData()
    {
        PersistentData persistentData = new PersistentData()
        {
            currentAtkBonus = pCombat.AtkBonus,
            currentHealth = pHealth.CurrentHealth,
            maxHealth = pHealth.MaxHealth
        };
        // Write to file
        File.WriteAllText(
            persistentDataPath,
            JsonUtility.ToJson(persistentData));
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            SaveData data = 
                JsonUtility
                .FromJson<SaveData>(
                    File.ReadAllText(savePath));

            // Load player position
            pCombat.transform.position = data.playerPos;
            // Load enemies who are alive
            eManager.LoadEnemies(data.aliveEnemies);
        }

        if (File.Exists(persistentDataPath))
        {
            PersistentData pData =
                JsonUtility
                .FromJson<PersistentData>(
                    File.ReadAllText(persistentDataPath));
            pCombat.Upgrade(pData.currentAtkBonus);
            pHealth.SetHealth(pData.currentHealth, pData.maxHealth);
        }
    }
}

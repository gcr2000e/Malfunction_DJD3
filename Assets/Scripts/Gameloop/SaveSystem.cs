using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    private string currentLevel;
    private bool canSave = true;

    private PlayerCombat pCombat;
    private PlayerHealth pHealth;

    private void Start()
    {
        pCombat = FindAnyObjectByType<PlayerCombat>();
        pHealth = FindAnyObjectByType<PlayerHealth>();
        SetLevel();
    }

    public void SetLevel()
    {
        currentLevel = SceneManager.GetActiveScene().name;
    }

    public void SetSaveStatus(bool status)
    {
        canSave = status;
    }

    public void AutoSave()
    {
        SetSaveStatus(true);
        SaveGame();
    }

    public void SaveGame()
    {
        float currentAtkBonus = pCombat.AtkBonus;
        int currentHealth = pHealth.CurrentHealth;
    }
}

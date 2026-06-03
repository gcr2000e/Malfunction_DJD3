using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private int currentLevel;
    private bool canSave = true;

    private PlayerCombat pCombat;
    private PlayerHealth pHealth;

    private void Start()
    {
        pCombat = FindAnyObjectByType<PlayerCombat>();
        pHealth = FindAnyObjectByType<PlayerHealth>();
    }

    public void SetLevel(int level)
    {
        currentLevel = level;
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

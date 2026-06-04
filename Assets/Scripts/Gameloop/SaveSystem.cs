using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private int currentLevel;
    private bool canSave = true;

    private PlayerCombat pCombat;
    private PlayerHealth pHealth;
    private GeneratorManager pcg;

    private void Start()
    {
        pCombat = FindAnyObjectByType<PlayerCombat>();
        pHealth = FindAnyObjectByType<PlayerHealth>();
        pcg = FindAnyObjectByType<GeneratorManager>();

        // If no save game then generate random
        pcg.Generate();
        // If there is a save game generate with the seed
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

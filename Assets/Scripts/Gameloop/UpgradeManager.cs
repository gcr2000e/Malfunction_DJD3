using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    private float attackBonus = 1.5f;
    [SerializeField]
    private int healthBonus = 50;

    private PlayerCombat pCombat;
    private PlayerHealth pHealth;

    private SaveSystem saveSystem;

    [SerializeField]
    private string nextLevel;

    private void Start()
    {
        saveSystem = FindAnyObjectByType<SaveSystem>();
        pCombat = FindAnyObjectByType<PlayerCombat>();
        pHealth = FindAnyObjectByType<PlayerHealth>();
    }

    public void SelectAttack()
    {
        pCombat.Upgrade(attackBonus);
        LoadNextLevel();
    }

    public void SelectHealth()
    {
        pHealth.IncreaseMaxHealth(healthBonus);
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        saveSystem.DeleteSave();
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextLevel);
    }
}

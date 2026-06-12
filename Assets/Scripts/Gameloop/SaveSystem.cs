using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    private PlayerCombat pCombat;
    private PlayerHealth pHealth;

    private EnemyManager eManager;

    private void Start()
    {
        pCombat = FindAnyObjectByType<PlayerCombat>();
        pHealth = FindAnyObjectByType<PlayerHealth>();
        eManager = FindAnyObjectByType<EnemyManager>();
    }

    public void SaveGame()
    {
        float currentAtkBonus = pCombat.AtkBonus;
        int currentHealth = pHealth.CurrentHealth;
        Vector3 playerPos = pCombat.transform.position;
        bool[] aliveEnemies = eManager.GetDeadEnemies();
        string currentLevel = SceneManager.GetActiveScene().name;
    }
}

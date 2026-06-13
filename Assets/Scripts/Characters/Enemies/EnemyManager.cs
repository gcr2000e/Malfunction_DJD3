using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    private EnemyHealth[] enemyList;

    private void Awake()
    {
        enemyList = 
            FindObjectsByType<EnemyHealth>
            (FindObjectsSortMode.InstanceID);
    }

    public void LoadEnemies(bool[] aliveEnemies)
    {
        int i = 0;
        Debug.Log(enemyList.Length);
        Debug.Log(aliveEnemies.Length);
        Debug.Log(aliveEnemies[0]);
        foreach (EnemyHealth enemy in enemyList)
        {
            enemy.LoadEnemy(aliveEnemies[i]);
            i++;
        }
    }

    public bool[] GetDeadEnemies()
    {
        return enemyList
            .Select(enemy => enemy.IsAlive)
            .ToArray();
    }
}

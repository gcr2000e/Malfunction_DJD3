using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    private EnemyHealth[] enemyList;

    private void Awake()
    {
        enemyList =
            FindObjectsByType<EnemyHealth>
            (FindObjectsSortMode.None)
                .OrderBy(e => e.transform.position.x)
                .ThenBy(e => e.transform.position.y)
                .ThenBy(e => e.transform.position.z)
                .ToArray();
    }

    public void LoadEnemies(bool[] aliveEnemies)
    {
        int i = 0;
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

using UnityEngine;
using System.Collections;

public class EMP : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DisableEMP());
    }

    IEnumerator DisableEMP()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        IEnemy enemy = other.GetComponent<IEnemy>();
        if (enemy != null)
        {
            enemy.Stun();
        }
    }
}

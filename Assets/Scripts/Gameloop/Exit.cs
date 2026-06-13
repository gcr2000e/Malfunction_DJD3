using UnityEngine;

public class Exit : MonoBehaviour
{
    private GameObject upgradeUI;

    private void Start()
    {
        upgradeUI = 
            FindAnyObjectByType<UpgradeManager>()
            .gameObject;
        upgradeUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            EndSequence();
        }
    }

    private void EndSequence()
    {
        Time.timeScale = 0f;
        upgradeUI.SetActive(true);
    }
}

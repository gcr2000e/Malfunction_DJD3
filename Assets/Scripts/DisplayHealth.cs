using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;

    private IHealth health;

    private void Start()
    {
        health = GetComponent<IHealth>();

        // Set max health
        healthSlider.maxValue = health.MaxHealth;
    }

    private void Update()
    {
        // Set display to current health
        healthSlider.value = health.CurrentHealth;
    }
}

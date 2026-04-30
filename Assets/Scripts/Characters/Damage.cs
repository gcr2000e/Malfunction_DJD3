using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    private uint damage;

    private IHealth health;

    private void Start()
    {
        health = GetComponentInParent<IHealth>();
    }

    // For variable damages
    public void SetDamage(uint damageValue)
    {
        damage = damageValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        IHealth otherHealth = other.GetComponent<IHealth>();

        // Prevent attacking own health system
        if (otherHealth != null 
            && otherHealth != health)
        {
            // Damage other
            otherHealth.Damage(damage);
        }
    }
}

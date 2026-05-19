using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private IHealth health;

    private void Start()
    {
        health = GetComponentInParent<IHealth>();
    }

    // For variable damages
    public void SetDamage(int damageValue)
    {
        damage = damageValue;
    }

    protected virtual void OnTriggerEnter(Collider other)
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

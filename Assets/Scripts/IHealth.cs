using UnityEngine;

public abstract class IHealth : MonoBehaviour
{
    public abstract uint MaxHealth { get; }
    public abstract uint CurrentHealth { get; }

    public abstract void Heal(uint healing);
    public abstract void Damage(uint damage);
}

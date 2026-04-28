public interface IHealth
{
    public uint MaxHealth { get; }
    public uint CurrentHealth { get; }

    public void Heal(uint healing);
    public void Damage(uint damage);
}

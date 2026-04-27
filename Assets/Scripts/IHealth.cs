public interface IHealth
{
    public int MaxHealth { get; }
    public void Heal();
    public void Damage();
}

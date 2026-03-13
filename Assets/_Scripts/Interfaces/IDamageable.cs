namespace _Scripts.Interfaces
{
    public interface IDamageable
    {
        public int GetCurrentHealth();
        public void SetCurrentHealth(int healthPoints);

        public int GetMaxHealth();
    }
}

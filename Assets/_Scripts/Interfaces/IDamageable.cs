namespace _Scripts.Interfaces
{
    public interface IDamageable
    {
        public float GetCurrentHealth();
        public void Damage(float healthPoints);
        public void Heal(float healthPoints);
        public float GetMaxHealth();
    }
}

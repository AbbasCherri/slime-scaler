using System;
using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.PlayerScripts
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [Header("Health")]
        [SerializeField] private int maxHealth;
        private int _currentHealth;
        private SpriteRenderer _sr;
        private Color _defaultColor;

        private void Start()
        {
            _sr =  gameObject.GetComponent<SpriteRenderer>();
            _defaultColor = _sr.color;
        }


        public int GetMaxHealth()
        {
            return maxHealth;
        }
        
        public int GetCurrentHealth()
        {
            return _currentHealth;
        }

        public void SetCurrentHealth(int healthPoints)
        {
            var net = _currentHealth + healthPoints;
        
            if (net >= maxHealth)
            {
                _sr.color = Color.green;
                _currentHealth = maxHealth;
                _sr.color = _defaultColor;
            } 
            else if (net  < 0)
            {
                _sr.color = Color.red;
                _currentHealth = 0;
                _sr.color = _defaultColor;
            }
            else if (net > _currentHealth)
            {
                _sr.color = Color.green;
                _currentHealth = net;
                _sr.color = _defaultColor;
            } 
            else if (net < _currentHealth)
            {
                _sr.color = Color.red;
                _currentHealth = net;
                _sr.color = _defaultColor;
            }
            else
            {
                _currentHealth = net;
            }
        }
        
    }
}

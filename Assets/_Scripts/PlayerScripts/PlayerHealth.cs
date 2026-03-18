using System;
using System.Collections;
using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.PlayerScripts

{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [Header("Health")]
        [SerializeField] private float maxHealth;
        [SerializeField] private float minHealth;
        private const float BaseHealth = 1;
        private float _currentHealth;
        private SpriteRenderer _sr;
        private Color _defaultColor;
        private bool _isDead;
        private static PlayerHealth _instance;
        
        [Header("Audio")]
        [SerializeField] private AudioClip healSound;


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }

            _instance = this;
        }


        private void Start()
        {
            _currentHealth = BaseHealth;
            _sr =  gameObject.GetComponent<SpriteRenderer>();
            _defaultColor = _sr.color;
            _isDead = false;
        }


        public float GetCurrentHealth()
        {
            return _currentHealth;
        }

        public void Damage(float healthPoints)
        {
            var net = _currentHealth - healthPoints;
            StartCoroutine(ColorFlash(Color.red, .3f));
            if (net < minHealth)
            {
                _currentHealth = minHealth;
                _isDead = true;
            }
            else
            {
                _currentHealth = net;
            }
            
            Scale(_currentHealth);
        }
        public void TakePercentageDamage(float percent)
        {
            float damage = BaseHealth * percent;
            Damage(damage);
        }

        public void Heal(float healthPoints)
        {
            var net = _currentHealth + healthPoints;
            StartCoroutine(ColorFlash(Color.green, .3f));

            _currentHealth = net >= maxHealth ? maxHealth : net;
            
            Scale(_currentHealth);
            
            if (AudioManager.Instance != null && healSound != null) 
            {
                AudioManager.Instance.sfxSource.PlayOneShot(healSound, 0.6f); // Sound on Heal
            }
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }
        
        
        private void Scale(float scale)
        {
            if (scale < 0) return;
            
            transform.localScale = new Vector2(scale, scale);
        }

        private IEnumerator ColorFlash(Color c, float t)
        {
            _sr.color = c;
            yield return new WaitForSeconds(t);
            _sr.color = _defaultColor;
        }

        public static PlayerHealth GetInstance()
        {
            return _instance;
        }
        
    }
}

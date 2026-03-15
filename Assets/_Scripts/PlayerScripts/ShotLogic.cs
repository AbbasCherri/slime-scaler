using System;
using UnityEngine;

namespace _Scripts.PlayerScripts
{
    public class ShotLogic : MonoBehaviour
    {
        [Header("Shot Attributes")]
        [SerializeField] private float lifeTime;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletDamage;
        private float _currentLifeTime;
        private GameObject _player;
        private Rigidbody2D _rb;
        private float _dir;
        
        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _dir = _player.transform.localScale.x;
            _rb = GetComponent<Rigidbody2D>();
        }


        private void Update()
        {
            
            ShotMovement();
            
            _currentLifeTime +=  Time.deltaTime;

            if (_currentLifeTime >= lifeTime)
            {
                Destroy(gameObject);
            } 
        }
 

        private void ShotMovement()
        {
            _rb.velocity = new Vector2( _dir * bulletSpeed, 0);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                PlayerHealth.GetInstance().Heal(.1f);
                Destroy(gameObject);
            }
            
        }
        
    }
}

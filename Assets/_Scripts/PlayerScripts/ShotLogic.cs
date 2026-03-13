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
            transform.Translate(new Vector2(0, 1) * (bulletSpeed * Time.deltaTime));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
        }
        
    }
}

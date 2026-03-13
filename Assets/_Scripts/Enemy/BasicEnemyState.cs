using System;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class BasicEnemyState : MonoBehaviour
    {
        [Header("Pathing")]
        [SerializeField] private GameObject obstacleCheck;
        [SerializeField] private float rayLength;
        [SerializeField] private float speed;
        private bool _isObstacle;
        private float _dir;
        private Rigidbody2D _rb;


        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            
            Pathing();
            
        }

        private void Pathing()
        {
            _dir = transform.localScale.x;
            
            _isObstacle = Physics2D.Raycast(obstacleCheck.transform.position, new Vector2(_dir, 1), rayLength, LayerMask.GetMask("Wall"));

            if (_isObstacle)
            {
                _dir *= -1;
                transform.localScale = new Vector2(_dir, 1);
                obstacleCheck.transform.localScale = new Vector2(_dir, 1);
            }
            
            _rb.velocity = new Vector2(_dir * speed, 0);
            
            
        }
        
    }

}

using _Scripts.PlayerScripts;
using UnityEngine;
using Pathfinding;

namespace _Scripts.Enemy
{
    public class BatEnemy : MonoBehaviour
    {
        [Header("Path Finding Settings")]
        private Transform _playerTarget;
        private Transform _home;
        private AIDestinationSetter _destinationSetter;
        private bool _isPlayerNear; 
        private float _currentTime;
        private bool _hitCollider; 
        private bool _isResting; 
        [SerializeField] private float aggroRadius;
        [SerializeField] private float coolDownTime;

        private void Start()
        {
            _playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
            _home = GameObject.FindGameObjectWithTag("BatHome").transform;
            _destinationSetter = GetComponent<AIDestinationSetter>();
        }

        private void Update()
        {
            // 1. Update Timer first
            if (_isResting)
            {
                _currentTime += Time.deltaTime;
                if (_currentTime >= coolDownTime)
                {
                    _isResting = false;
                    _hitCollider = false;
                    _currentTime = 0;
                }
            }

            PathLogic();
        }

        private void PathLogic()
        {
            var playerLayerMask = 1 << LayerMask.NameToLayer("Player");
            _isPlayerNear = Physics2D.OverlapCircle(transform.position, aggroRadius, playerLayerMask);

            if (_isPlayerNear && !_hitCollider && !_isResting)
            {
                _destinationSetter.target = _playerTarget;
            }
            else
            {
                _destinationSetter.target = _home;
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player")) 
            {
                Debug.Log("Bat: Hit Player! Going home.");
                _hitCollider = true;
                PlayerHealth.GetInstance().Damage(.1f);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("BatHome") && _hitCollider)
            {
                Debug.Log("Bat: Arrived at home. Starting cooldown.");
                _isResting = true;
                _currentTime = 0; 
            }
        }

        // private void OnDrawGizmosSelected()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawWireSphere(transform.position, aggroRadius);
        // }
    }
}
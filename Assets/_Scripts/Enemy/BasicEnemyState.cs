using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class BasicEnemyState : MonoBehaviour
    {
        [Header("Pathing")]
        [SerializeField] private GameObject obstacleCheck;
        [SerializeField] private float rayLength;
        [SerializeField] private float speed;
        [SerializeField] private bool _isObstacle;
        private float _dir;
        private Rigidbody2D _rb;
        [Header("Attacking")] 
        [SerializeField] private GameObject playerCheck;
        [SerializeField] private float playerRayLength;
        [SerializeField] [Range(0,1)] private float damage;
        [SerializeField] private float attackSpeed;
        [SerializeField] private bool _isPlayerPresent;
        [SerializeField] private Animator animator;


        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            
            BasicPathing();
            
            BasicAttacking();
            animator.Play("Fly");
        }

        private void BasicPathing()
        {
            _dir = transform.localScale.x;

            Debug.DrawRay(playerCheck.transform.position, new Vector2(_dir, 0) * rayLength, Color.red);

            _isObstacle = Physics2D.Raycast(obstacleCheck.transform.position, new Vector2(_dir, 0), rayLength,
                LayerMask.GetMask("Ground", "Hybrid"));

            if (_isObstacle)
            {
                _dir *= -1;
                transform.localScale = new Vector3(_dir, 1, 1);
                obstacleCheck.transform.localScale = new Vector3(_dir, 1, 1);
                playerCheck.transform.localScale = new Vector3(_dir, 1, 1);
            }
            
            _rb.velocity = new Vector2(_dir * speed, 0);
        }

        private void BasicAttacking()
        {
            // Debug.DrawRay(playerCheck.transform.position, new Vector2(_dir, 0) * playerRayLength, Color.red);

            _isPlayerPresent = Physics2D.Raycast(playerCheck.transform.position, new Vector2(_dir, 0), playerRayLength,
                LayerMask.GetMask("Player"));

            if (_isPlayerPresent)
            {
                _rb.velocity = new Vector2(_dir * attackSpeed, 0);
            }
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            PlayerHealth.GetInstance().Damage(damage);
            Destroy(gameObject);
        }
    }

}

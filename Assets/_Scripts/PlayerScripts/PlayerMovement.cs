using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")] [SerializeField] private float speed;
        [SerializeField] private float sprintSpeed;
        private float _movementSpeed;
        private Rigidbody2D _rb;
        private float _xInput;
        private int _facingDirection;
        [Header("Jump")] [SerializeField] private float jumpForce;
        [SerializeField] private float groundRadius;
        [SerializeField] private GameObject groundCheck;
        [SerializeField] private float gravityMultiplier;
        [SerializeField] private float lowJumpMultiplier;
        [SerializeField] private float coyoteTime;
        private float _currentCoyoteTime;
        [SerializeField] private float reserveTime;
        private float _currentReserveTime;
        private bool _reservedJump;
        private bool _isGrounded;
        [Header("Wall Jump")]
        [FormerlySerializedAs("rightWallCheck")]
        [SerializeField] private GameObject wallCheck;
        [SerializeField] private float wallCheckRadius;
        [SerializeField] [Range(0,1)] private float slideSpeed;
        [SerializeField] private float horizonWallJumpingSpeed; 
        [SerializeField] private float verticalWallJumpingSpeed;
        [SerializeField] private float wallJumpDuration;
        private bool _isWallJumping;
        private float _currentWallTime;
        private bool _isOnWall;

        private void Start()
        {
            _facingDirection = 1;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // Movement
            _xInput = Input.GetAxisRaw("Horizontal");
            _movementSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;

            // Jump
            JumpLogic();

            // Wall Jump Logic
            WallJumpLogic();

        }

        private void FixedUpdate()
        {
            if (!_isWallJumping)
            {
                _facingDirection = _xInput switch
                {
                    > 0 => 1,
                    < 0 => -1,
                    _ => _facingDirection
                };

                transform.localScale = new Vector3(_facingDirection * Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                _rb.velocity = new Vector2(_xInput * _movementSpeed, _rb.velocity.y);
            }
        }

        private void Jump()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }

        private void WallJump(float direction)
        {
            _isWallJumping = true;
            _currentWallTime = 0;
            _rb.velocity = new  Vector2(direction * horizonWallJumpingSpeed, verticalWallJumpingSpeed);
        }


        private void JumpLogic()
        {
            _isGrounded =
                Physics2D.OverlapCircle(groundCheck.transform.position, groundRadius, LayerMask.GetMask("Ground"));

            // Reserved Jump Time Logic
        
            if (_reservedJump)
            {
                _currentReserveTime += Time.deltaTime;
            }
            else
            {
                _currentReserveTime = 0;
            }
        
        
            // Coyote Time Logic
        
            if (!_isGrounded)
            {
                _currentCoyoteTime += Time.deltaTime;
            }
            else
            {
                if (_reservedJump && _currentReserveTime < reserveTime) // Reserve Payoff and Relief
                {
                    Jump();
                    _reservedJump = false;
                }
            
                _currentCoyoteTime = 0; 
            }

        
            // Jump Logic 
        
            if (Input.GetButtonDown("Jump"))
            {
                if (_currentCoyoteTime < coyoteTime) // Coyote Time Check
                {
                    Jump();
                }
                else // Reserve Jump
                {
                    _reservedJump = true;
                }
            
            }

        
            // Gravity Logic
            switch (_rb.velocity.y)
            {
                case < 0:
                    _rb.velocity += Vector2.up * (Physics2D.gravity.y * (gravityMultiplier - 1) * Time.deltaTime);
                    break;
                case > 0 when !Input.GetButton("Jump"):
                    _rb.velocity += Vector2.up * (Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
                    break;
            }
        }

        private void WallJumpLogic()
        {
            Debug.DrawRay(wallCheck.transform.position, new Vector2(_facingDirection * wallCheckRadius, 0), Color.red);
            
            _isOnWall = Physics2D.Raycast(wallCheck.transform.position, new Vector2(_facingDirection * wallCheckRadius, 0),
                wallCheckRadius,  LayerMask.GetMask("Ground"));

            if (_isGrounded) return;
            
            if (_isWallJumping)
            {
                _currentWallTime += Time.deltaTime;
                if (_currentWallTime >= wallJumpDuration)
                {
                    _isWallJumping = false;
                }
            }
            

            if (_isOnWall && Input.GetButtonDown("Jump"))
            {
                WallJump(-_facingDirection);
            }
            
            if (_isOnWall && !_isWallJumping)
            {
                _rb.velocity = new Vector2(transform.position.x, Physics2D.gravity.y * slideSpeed);
            }
            
        }
    }
}

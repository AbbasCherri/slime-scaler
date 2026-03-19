using System;
using _Scripts.Sound;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")] 
        [SerializeField] private float speed;
        [SerializeField] private float sprintSpeed;
        private float _ogSpeed;
        private float _ogSprint;
        private float _movementSpeed;
        private Rigidbody2D _rb;
        private float _xInput;
        private int _facingDirection;
        private static PlayerMovement _instance;
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
    	private bool _isSprinting;
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
        [Header("Animations")]
        public Animator animator;
        [Header("Audio")]
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip moveSound;
        [SerializeField] private AudioClip music;
        private float _count = 0;
        [Header("Trap Respawn")]
        [SerializeField] private float checkpointUpdateInterval = 0.2f;
        private Vector2 _lastSafePosition;
        private float _checkpointTimer;
        private float _moveTimer;
        [Header("Particle")] 
        [SerializeField] private ParticleSystem smoke;


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
            _facingDirection = 1;
            _rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            smoke = GetComponentInChildren<ParticleSystem>();
            _ogSpeed = speed;
            _ogSprint = sprintSpeed;
            _lastSafePosition = _rb.position;
        }

        private void Update()
        {
            // Movement
            _xInput = Input.GetAxisRaw("Horizontal");
            _movementSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;
            
            // Movement Sound
            if (Mathf.Abs(_xInput) > 0.1f && _isGrounded)
            {
                _moveTimer -= Time.deltaTime;

                if (_moveTimer <= 0f)
                {
                    if (_count == 0)
                    {
                        AudioManager.Instance.PlayMusic(music);
                        _count++;
                    }
                    AudioManager.Instance.PlaySfx(moveSound);
                    _moveTimer = 0.7f; 
                }
            }
            else
            {
                _moveTimer = 0f;
            }

            // Jump
            JumpLogic();

            // Wall Jump Logic
            WallJumpLogic();
	    _isSprinting = Input.GetKey(KeyCode.LeftShift) ? true: false;
	    //Animator Logic
            animator.SetFloat("Speed", Math.Abs(_rb.velocity.x));
            animator.SetFloat("yVel", _rb.velocity.y);
            animator.SetBool("Grounded", _isGrounded);
	    animator.SetBool("Sprint",_isSprinting);
        SaveSafePosition();

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
                if (_rb.velocity.y == 0)
                {
                    smoke.Play();
                }
            }
        }

        private void Jump()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
            smoke.Play();
            
            AudioManager.Instance.PlaySfx(jumpSound); //Sound when jump
        }

        private void WallJump(float direction)
        {
            _isWallJumping = true;
            _currentWallTime = 0;
            _rb.velocity = new  Vector2(direction * horizonWallJumpingSpeed, verticalWallJumpingSpeed);
            animator.SetTrigger("Jump");
            smoke.Play();
            
            AudioManager.Instance.PlaySfx(jumpSound); // Sound when jump
        }


        private void JumpLogic()
        {
            _isGrounded =
                Physics2D.OverlapCircle(groundCheck.transform.position, groundRadius, 
                    LayerMask.GetMask("Ground", "Hybrid"));

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
                wallCheckRadius,  LayerMask.GetMask("Ground", "Hybrid"));

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


        public float GetFacingDirection()
        {
            return _facingDirection;
        }
        private void SaveSafePosition()
        {
            _checkpointTimer += Time.deltaTime;

            if (_checkpointTimer >= checkpointUpdateInterval && _isGrounded && !IsStandingOnMovingPlatform())
            {
                _lastSafePosition = _rb.position;
                _checkpointTimer = 0f;
            }
        }

        public void RespawnAtLastSafePosition()
        {
            _rb.position = _lastSafePosition;
            _rb.velocity = Vector2.zero;
        }
        private bool IsStandingOnMovingPlatform()
        {
            Collider2D hit = Physics2D.OverlapCircle(
                groundCheck.transform.position,
                groundRadius,
                LayerMask.GetMask("Ground", "Hybrid")
            );

            if (hit == null)
                return false;

            return hit.GetComponent<MovingPlat>() != null || hit.GetComponentInParent<MovingPlat>() != null;
        }

        public void SetSpeed(float newSpeed)
        {
            speed *= newSpeed;
            sprintSpeed *= newSpeed;
        }

        public void ResetSpeed()
        {
            speed = _ogSpeed;
            sprintSpeed = _ogSprint;
        }

        public static PlayerMovement GetInstance()
        {
            return _instance;
        }
    }
}

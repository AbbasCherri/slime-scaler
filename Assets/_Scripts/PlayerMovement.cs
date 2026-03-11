using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] [SerializeField] private float speed;
    [SerializeField] private float sprintSpeed;
    private float _movementSpeed;
    private Rigidbody2D _rb;
    private float _xInput;
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
    [Header("Wall Jump")] [SerializeField] private GameObject leftWallCheck;
    [SerializeField] private GameObject rightWallCheck;
    [SerializeField] private float wallCheckRadius;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float horizonWallJumpingSpeed; 
    [SerializeField] private float verticalWallJumpingSpeed;
    [SerializeField] private float wallJumpDuration;
    private bool _isWallJumping;
    private float _currentWallTime;
    private bool _isRightWall;
    private bool _isLeftWall;

    private void Start()
    {
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
            _rb.velocity = new Vector2(_xInput * _movementSpeed, _rb.velocity.y);
        }
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }

    private void WallJump(int direction)
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
        _isLeftWall = Physics2D.Raycast(leftWallCheck.transform.position, new Vector2(-wallCheckRadius, 0),
            wallCheckRadius,  LayerMask.GetMask("Wall"));
        _isRightWall = Physics2D.Raycast(rightWallCheck.transform.position, new Vector2(wallCheckRadius, 0),
            wallCheckRadius,  LayerMask.GetMask("Wall"));

        if (_isWallJumping)
        {
            _currentWallTime += Time.deltaTime;
            if (_currentWallTime >= wallJumpDuration)
            {
                _isWallJumping = false;
            }
        }

        if (_isLeftWall || _isRightWall)
        {
            _rb.velocity += Vector2.up * (Physics2D.gravity.y * slideSpeed * Time.deltaTime);
        }

        if (_isGrounded) return;
        
        if (_isLeftWall && Input.GetButtonDown("Jump"))
        {
            WallJump(1);
        }

        if (_isRightWall && Input.GetButtonDown("Jump"))
        {
            WallJump(-1);
        }
    }
}

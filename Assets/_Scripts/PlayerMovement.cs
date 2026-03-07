using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float sprintSpeed;
    private float _movementSpeed;
    private Rigidbody2D _rb;
    private float _xInput;
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundRadius;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private float lowJumpMultiplier;
    private bool _isGrounded;

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
        
        
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_xInput * _movementSpeed, _rb.velocity.y);
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }

    private void JumpLogic()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundRadius, LayerMask.GetMask("Ground"));
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Jump();
        }
    }
}

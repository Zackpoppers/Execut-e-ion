using UnityEngine;

public class PlayerMovementTemp : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] float groundCheckDistance = 0.1f;

    Rigidbody2D rb;
    PlayerInput playerInput;
    bool isGrounded;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void Update()
    {
        if (playerInput.JumpPressed && isGrounded)
        {
            Jump();
        }
    }

    public void FixedUpdate()
    {
        HandleMovement();
        CheckGrounded();
    }

    public void HandleMovement()
    {
        Vector2 moveInput = playerInput.MoveInput;
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    public void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) ||
                     Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
}

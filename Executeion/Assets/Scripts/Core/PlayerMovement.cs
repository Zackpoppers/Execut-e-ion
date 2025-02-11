using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement variables
    public float acceleration;
    public float groundSpeed;
    public float jumpSpeed;
    [Range(0f, 1f)] 
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public float groundDecay;
    public bool isGrounded;
    public float horizontalInput;
    public float verticalInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandleJump();
    }

    void MoveWithInput()
    {
        if (Mathf.Abs(horizontalInput) > 0)
        {
            float increment = horizontalInput * acceleration;
            float newSpeed = Mathf.Clamp(body.linearVelocityX + increment, -groundSpeed, groundSpeed);
            body.linearVelocity = new Vector2(newSpeed, body.linearVelocityY);

            float direction = Mathf.Sign(horizontalInput);
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocityX, jumpSpeed);
        }
    }

    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        CheckGround();
        MoveWithInput();
        ApplyFriction();
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    void ApplyFriction()
    {
        if (isGrounded && horizontalInput == 0 && verticalInput <= 0)
        {
            body.linearVelocity *= groundDecay;
        }
    }
}

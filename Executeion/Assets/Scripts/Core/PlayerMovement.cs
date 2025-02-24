using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement variables
    public float acceleration;
    public float airAcceleration;
    public float groundSpeed;
    public float airSpeed;
    public float jumpSpeed;
    [Range(0f, 1f)]
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public float groundDecay;
    public bool isGrounded;
    public float horizontalInput;
    public float verticalInput;

    // Input variables
    public char downKey;

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


    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocityX, jumpSpeed);
        }
        else if (KeyCode.TryParse(downKey.ToString().ToUpper(), out KeyCode downButton))
        {
            if (Input.GetKeyDown(downButton)&& !isGrounded)
            {
                float increment = verticalInput * acceleration;
                body.linearVelocity = new Vector2(body.linearVelocityX + increment, -jumpSpeed / 2);
            }
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
    void MoveWithInput()
    {
        if (Mathf.Abs(horizontalInput) > 0)
        {
            float increment = horizontalInput * acceleration;
            float newSpeed = Mathf.Clamp(body.linearVelocityX + increment, -groundSpeed, groundSpeed);
            body.linearVelocity = new Vector2(newSpeed, body.linearVelocityY);

            float direction = Mathf.Sign(horizontalInput);
            transform.localScale = new Vector3(direction, 1, 1);

            if(!isGrounded)
            {
                float airIncrement = horizontalInput * airAcceleration;
                float newAirSpeed = Mathf.Clamp(body.linearVelocityX + airIncrement, -airSpeed, airSpeed);
                body.linearVelocity = new Vector2(newAirSpeed, body.linearVelocityY);
            }
        }
    }

    void ApplyFriction()
    {
        if (isGrounded && horizontalInput == 0 && verticalInput <= 0)
        {
            body.linearVelocity *= groundDecay;
        }
    }
}

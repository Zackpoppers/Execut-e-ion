using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Movement variables
    public float currentSpeed;
    public float acceleration;
    public float airAcceleration;
    public float groundSpeed;
    public float airSpeed;
    public float jumpSpeed;
    [Range(0f, 1f)]
    public float smoothedSpeed;
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public float groundDecay;
    public bool isGrounded;
    public bool isPlayer1;

    // Input variables
    public Vector2 moveInput;
    public PlayerControls playerControls;
    public InputAction move;
    public InputAction jump;
    public InputAction down;

    //Input Setup
    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        if (isPlayer1)
        {
            move = playerControls.Player1.Move;
            move.Enable();

            jump = playerControls.Player1.Jump;
            jump.Enable();
            jump.performed += Jump;

            down = playerControls.Player1.Down;
            down.Enable();
            down.performed += Down;
        }
        else
        {
            move = playerControls.Player2.Move;
            move.Enable();

            jump = playerControls.Player2.Jump;
            jump.Enable();
            jump.performed += Jump;

            down = playerControls.Player2.Down;
            down.Enable();
            down.performed += Down;
        }
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        down.Disable();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocityX, jumpSpeed);
            isGrounded = false;
        }
    }

    private void Down(InputAction.CallbackContext context)
    {
        if (!isGrounded)
        {
            float increment = body.linearVelocityY * acceleration;
            body.linearVelocity = new Vector2(body.linearVelocityX + increment, -jumpSpeed * 1.5f);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = move.ReadValue<Vector2>();
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
        if (Mathf.Abs(moveInput.x) > 0)
        {
            float targetSpeed = moveInput.x * groundSpeed;
            smoothedSpeed = Mathf.SmoothDamp(body.linearVelocityX, targetSpeed, ref currentSpeed, acceleration);
            body.linearVelocity = new Vector2(smoothedSpeed, body.linearVelocityY);

            float direction = Mathf.Sign(moveInput.x);
            transform.localScale = new Vector3(direction, 1, 1);

            if (!isGrounded)
            {
                float targetAirSpeed = moveInput.x * airSpeed;
                smoothedSpeed = Mathf.SmoothDamp(body.linearVelocityX, targetAirSpeed, ref currentSpeed, airAcceleration);
                body.linearVelocity = new Vector2(smoothedSpeed, body.linearVelocityY);
            }
        }
    }

    void ApplyFriction()
    {
        if (isGrounded && moveInput.x == 0 && body.linearVelocityY <= 0)
        {
            body.linearVelocity *= groundDecay;
        }
    }
}

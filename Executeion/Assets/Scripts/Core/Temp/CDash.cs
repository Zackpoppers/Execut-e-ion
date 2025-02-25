using UnityEngine;

public class CDash : MonoBehaviour
{
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCooldown = 1f;

    Rigidbody2D rb;
    PlayerInput playerInput;
    bool isDashing;
    float dashTimer;
    float cooldownTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput != null && playerInput.AttackPressed && !isDashing && cooldownTimer <= 0)
        {
            StartDash();
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                EndDash();
            }
        }

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        float direction = transform.localScale.x > 0 ? 1 : -1;
        rb.linearVelocity = new Vector2(direction * dashSpeed, rb.linearVelocity.y);
    }

    public void EndDash()
    {
        isDashing = false;
        cooldownTimer = dashCooldown;
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }
}

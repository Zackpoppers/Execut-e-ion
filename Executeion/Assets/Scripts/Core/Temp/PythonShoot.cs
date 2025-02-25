using UnityEngine;

public class PythonShoot : MonoBehaviour
{
    [SerializeField] GameObject snakeProjectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float projectileDamage = 20f;

    PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput != null && playerInput.AttackPressed)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(snakeProjectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.right * projectileSpeed;

        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetDamage(projectileDamage);
            projectileScript.SetShooter(gameObject);
        }
    }
}

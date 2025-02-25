using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float damage = 20f;
    [SerializeField] float lifetime = 3f;

    GameObject shooter;

    public void SetShooter(GameObject player)
    {
        shooter = player;
    }

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == shooter)
        {
            return;
        }

        PlayerTemp player = collision.GetComponent<PlayerTemp>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetDamage(float projectileDamage)
    {
        this.damage = projectileDamage;
    }
}

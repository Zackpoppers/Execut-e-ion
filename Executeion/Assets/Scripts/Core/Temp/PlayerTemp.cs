using UnityEngine;

public class PlayerTemp : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float currentHealth;
    [SerializeField] GameObject healthBarPrefab;

    public float MoveSpeed { get; set; }
    public float JumpForce { get; set; }

    private GameObject healthBar;
    private Transform uiCanvas;

    private void Start()
    {
        currentHealth = maxHealth;
        uiCanvas = GameObject.Find("UI").transform;
        healthBar = Instantiate(healthBarPrefab, uiCanvas);
    }

    public void Update()
    {
        healthBar.GetComponent<UIPoolBar>().SetFillAmount(currentHealth / maxHealth);
        healthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 1.2f, 0));
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}

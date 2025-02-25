using UnityEngine;

public class CSharpHeal : MonoBehaviour
{
    [SerializeField] float healAmount = 10f;
    [SerializeField] float healCooldown = 5f;

    float cooldownTimer;
    PlayerTemp player;

    public void Start()
    {
        player = GetComponent<PlayerTemp>();
        cooldownTimer = healCooldown;
    }

    public void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            Heal();
            cooldownTimer = healCooldown;
        }
    }

    public void Heal()
    {
        if (player != null)
        {
            player.RestoreHealth(healAmount);
        }
    }
}

using UnityEngine;

public class Melon : ItemBase
{
    [SerializeField] private int healthRegenModifier = 1;

    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }
    protected override void GetItem()
    {
        if (playerHealth.CurrentHealth >= playerHealth.MaxHealth || playerHealth.CurrentHealth < 0)
        {
            return;
        }

        playerHealth.AddHealth(healthRegenModifier);
        Destroy(gameObject);
    }
}

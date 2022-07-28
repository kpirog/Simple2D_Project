using UnityEngine;

public class Melon : ItemBase
{
    [SerializeField] private int healthRegenModifier = 1;
    [SerializeField] private AudioClip eatingClip;

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

        AudioSystem.PlaySFX_Global(eatingClip);
        playerHealth.AddHealth(healthRegenModifier);
        Destroy(gameObject);
    }
}

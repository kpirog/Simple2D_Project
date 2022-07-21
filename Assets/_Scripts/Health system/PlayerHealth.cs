using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private HealthView healthView;

    private int currentHealth;
    public int CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthView.onHealthUpdated?.Invoke(CurrentHealth);
    }
    [ContextMenu("Take Damage")]
    public void TakeDamage()
    {
        currentHealth--;
        healthView.onHealthUpdated?.Invoke(CurrentHealth);
    }
}

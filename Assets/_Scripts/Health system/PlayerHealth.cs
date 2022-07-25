using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private HealthView healthView;
    [SerializeField] private PlayerStateMachine playerStateMachine;

    private int currentHealth;
    public int CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthView.onHealthUpdated?.Invoke(CurrentHealth);
    }
    public void TakeDamage()
    {
        currentHealth--;
        healthView.onHealthUpdated?.Invoke(CurrentHealth);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform enemy = collision.transform;
        
        if (enemy.CompareTag("Mushroom"))
        {
            TakeDamage();
            playerStateMachine.PushPlayerInDirection(enemy.right);
        }
    }
}

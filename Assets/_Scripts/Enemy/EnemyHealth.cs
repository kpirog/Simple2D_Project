using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 2;

    private int currentHealth;

    public int CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage()
    {
        if (currentHealth <= 0) { return; }

        currentHealth--;
    }
}

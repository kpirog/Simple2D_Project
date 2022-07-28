using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private PlayerStateMachine playerStateMachine;

    [SerializeField] private AudioClip getHitClip;

    private int currentHealth;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Start()
    {
        RestoreFullHealth();
    }
    private void OnEnable()
    {
        EventManager.OnRoundStart += EventManager_OnRoundStart;
        EventManager.OnPlayerHitEvent += TakeDamage;;
    }
    private void OnDisable()
    {
        EventManager.OnRoundStart -= EventManager_OnRoundStart;
        EventManager.OnPlayerHitEvent -= TakeDamage;
    }
    private void EventManager_OnRoundStart()
    {
        if (currentHealth < maxHealth)
        {
            RestoreFullHealth();
        }
    }
    private void TakeDamage()
    {
        currentHealth--;

        AudioSystem.PlaySFX_Global(getHitClip);
        EventManager.OnHeartUpdated(CurrentHealth);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform enemy = collision.transform;

        if (enemy.CompareTag("Mushroom"))
        {
            TakeDamage();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Mushroom") && currentHealth > 0)
        {
            playerStateMachine.PushPlayerInDirection(collision.transform.position);
        }
    }
    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        EventManager.OnHeartUpdated(CurrentHealth);
    }
    public void AddHealth(int value)
    {
        currentHealth += value;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        EventManager.OnHeartUpdated(CurrentHealth);
    }
}

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 2;

    private Animator anim;
    private int getHitAnimationKey = Animator.StringToHash("GetHit");

    private int currentHealth;
    public int CurrentHealth => currentHealth;

    private void Start()
    {
        anim = GetComponent<Animator>();
        ReviveEnemy();
    }
    private void OnEnable()
    {
        EventManager.OnRoundStart += EventManager_OnRoundStart;
    }
    private void EventManager_OnRoundStart()
    {
        ReviveEnemy();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerWeapon"))
        {
            TakeDamage();
        }
    }
    public void TakeDamage()
    {
        if (currentHealth <= 0) { return; }

        anim.SetTrigger(getHitAnimationKey);

        currentHealth--;
    }
    public void ReviveEnemy()
    {
        currentHealth = maxHealth;
    }
}

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
        currentHealth = maxHealth;
    }
    public void TakeDamage()
    {
        if (currentHealth <= 0) { return; }

        anim.SetTrigger(getHitAnimationKey);

        currentHealth--;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerWeapon"))
        {
            TakeDamage();
        }
    }
}

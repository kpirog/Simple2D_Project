using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private PlayerStateMachine playerStateMachine;

    private int currentHealth;
    public int CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        EventManager.OnHeartUpdated(CurrentHealth);
    }
    public void TakeDamage()
    {
        currentHealth--;
        EventManager.OnHeartUpdated(CurrentHealth);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform enemy = collision.transform;

        if (enemy.CompareTag("Mushroom") || enemy.CompareTag("EnemyWeapon"))
        {
            //TakeDamage();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Mushroom") && currentHealth > 0)
        {
            playerStateMachine.SetPlayerXConstraint(false);
            playerStateMachine.PushPlayerInDirection(collision.transform.position);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Mushroom"))
        {
            StartCoroutine(ToggleCollsionContraintsAfterTime(true, 1f));
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    private IEnumerator ToggleCollsionContraintsAfterTime(bool value, float time)
    {
        yield return new WaitForSeconds(time);
        playerStateMachine.SetPlayerXConstraint(value);
    }
}

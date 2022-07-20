using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private int attackAnimationKey = Animator.StringToHash("Attack");
    
    private Animator anim;
    private PlayerControls playerControls;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Gameplay.Attack.started += ctx => Attack();
    }
    private void OnDisable()
    {
        playerControls.Gameplay.Attack.started -= ctx => Attack();
        playerControls.Disable();
    }
    private void Attack()
    {
        anim.SetTrigger(attackAnimationKey);
    }
}

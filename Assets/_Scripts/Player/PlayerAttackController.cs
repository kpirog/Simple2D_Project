using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D weaponCollider;

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
    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
    }
    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
    }
    public void SetColliderDirection(bool isLeft)
    {
        Vector3 colliderPos = weaponCollider.offset;

        if ((isLeft && colliderPos.x > 0f) || (!isLeft && colliderPos.x < 0f))
        {
            colliderPos.x *= -1f;
        }
        else
        {
            return;
        }

        weaponCollider.offset = colliderPos;
    }
}

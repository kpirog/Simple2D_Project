using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D weaponCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
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
        SetColliderDirection(spriteRenderer.flipX);

        weaponCollider.enabled = true;
    }
    public void DisableWeaponCollider()
    {
        SetColliderDirection(false);
        
        weaponCollider.enabled = false;
    }
    private void SetColliderDirection(bool leftDirection)
    {
        Vector3 colliderPos = weaponCollider.offset;

        colliderPos.x = leftDirection ? -colliderPos.x : colliderPos.x;

        weaponCollider.offset = colliderPos;
    }
}

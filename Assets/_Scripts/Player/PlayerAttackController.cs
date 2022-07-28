using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D weaponCollider;

    [SerializeField] private AudioClip swordAttackClip;
    [SerializeField] private AudioClip shurikenClip;

    private Shuriken shuriken;
    private int attackAnimationKey = Animator.StringToHash("Attack");

    private Animator anim;
    private PlayerControls playerControls;

    public bool HasAlreadyShuriken => shuriken != null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Gameplay.Attack.started += ctx => Attack();
        playerControls.Gameplay.ThrowShuriken.started += ctx => ThrowShuriken();
    }
    private void OnDisable()
    {
        playerControls.Gameplay.Attack.started -= ctx => Attack();
        playerControls.Gameplay.ThrowShuriken.started -= ctx => ThrowShuriken();
        playerControls.Disable();
    }
    private void Attack()
    {
        anim.SetTrigger(attackAnimationKey);
        AudioSystem.PlaySFX_Global(swordAttackClip);
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
    public void SetShuriken(Shuriken shuriken)
    {
        this.shuriken = shuriken;
    }
    public void ThrowShuriken()
    {
        if (shuriken != null)
        {
            shuriken.Throw();
            shuriken = null;
            AudioSystem.PlaySFX_Global(shurikenClip);
        }
    }
}

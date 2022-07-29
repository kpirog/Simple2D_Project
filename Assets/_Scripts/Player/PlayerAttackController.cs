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
    private GamepadCursor gamepadCursor;

    public bool HasAlreadyShuriken => shuriken != null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerControls = new PlayerControls();
        gamepadCursor = FindObjectOfType<GamepadCursor>();
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
            Vector2 direction = (gamepadCursor.CurrentCursorPosition - (Vector2)transform.position).normalized;

            shuriken.Throw(direction);
            shuriken = null;
            
            AudioSystem.PlaySFX_Global(shurikenClip);
        }
    }
}

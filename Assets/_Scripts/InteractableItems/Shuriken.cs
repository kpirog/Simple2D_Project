using UnityEngine;

public class Shuriken : ItemBase
{
    [SerializeField] private CircleCollider2D collisionCollider;
    [SerializeField] private float throwSpeed = 10f;

    private bool canDestroy = false;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private PlayerAttackController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerAttackController>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.OnGetShurikenEvent += EventManager_OnGetShurikenEvent;
        EventManager.OnThrowShurikenEvent += EventManager_OnThrowShurikenEvent; 
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        EventManager.OnGetShurikenEvent -= EventManager_OnGetShurikenEvent;
        EventManager.OnThrowShurikenEvent -= EventManager_OnThrowShurikenEvent;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.HasAlreadyShuriken) { return; }

        base.OnTriggerEnter2D(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && canDestroy)
        {
            Destroy(gameObject);
        }
    }
    private void EventManager_OnGetShurikenEvent()
    {
        rb.simulated = false;
    }
    private void EventManager_OnThrowShurikenEvent()
    {
        rb.simulated = true;
    }
    protected override void GetItem()
    {
        SetSpriteAndParent(true);
        SetPhysicsSettings(true);
        
        transform.position = player.transform.position;
        player.SetShuriken(this);

        EventManager.OnGetShuriken();
    }
    public void Throw(Vector2 direction)
    {
        canDestroy = true;

        SetSpriteAndParent(false);
        SetPhysicsSettings(false);

        rb.AddForce(direction * throwSpeed, ForceMode2D.Impulse);
        EventManager.OnThrowShuriken();
    }
    private void SetPhysicsSettings(bool isPickedUp)
    {
        rb.bodyType = isPickedUp ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
        rb.simulated = !isPickedUp;

        collisionCollider.enabled = !isPickedUp;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Shuriken"), !isPickedUp);
    }
    private void SetSpriteAndParent(bool isPickedUp)
    {
        spriteRenderer.enabled = !isPickedUp;

        Transform parent = isPickedUp ? player.transform : player.transform.parent;
        transform.SetParent(parent);
    }
    private void OnDestroy()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Shuriken"), false);
    }
}

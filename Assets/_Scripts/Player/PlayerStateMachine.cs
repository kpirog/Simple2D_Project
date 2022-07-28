using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float pushForce;

    [Header("Ground check settings")]
    [SerializeField] private float groundCheckDistance = -0.5f;
    [SerializeField] private Vector3 leftRayPosition;
    [SerializeField] private Vector3 rightRayPosition;
    [SerializeField] private LayerMask groundLayerMask;

    private BasePlayerState currentState;

    private bool isActive;
    private Vector2 movementDirection;
    public Vector2 MovementDirection => movementDirection;

    private PlayerHealth playerHealth;
    private PlayerAttackController playerAttackController;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private PlayerControls playerControls;
    private InputAction jumpAction;
    private InputAction moveAction;

    [HideInInspector] public Animator anim;
    public bool IsJumping => jumpAction.triggered && rb.velocity.y > 0f;
    public bool IsFallingDown => rb.velocity.y < 0f;

    public bool IsAlive => playerHealth.CurrentHealth > 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
        playerAttackController = GetComponent<PlayerAttackController>();
        playerControls = new PlayerControls();
        moveAction = playerControls.Gameplay.Move;
        jumpAction = playerControls.Gameplay.Jump;
    }
    private void Start()
    {
        SwitchState(new PlayerIdleState(this));
    }
    private void OnEnable()
    {
        playerControls.Enable();
        moveAction.performed += ctx => SetSpriteDirection(ctx.ReadValue<Vector2>().x);
        moveAction.performed += ctx => movementDirection = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => movementDirection = ctx.ReadValue<Vector2>();
        jumpAction.started += ctx => Jump();
        EventManager.OnRoundStart += EventManager_OnRoundStart;
        EventManager.OnRoundComplete += EventManager_OnRoundComplete;
    }
    private void OnDisable()
    {
        playerControls.Disable();
        moveAction.performed -= ctx => SetSpriteDirection(ctx.ReadValue<Vector2>().x);
        moveAction.performed -= ctx => movementDirection = ctx.ReadValue<Vector2>();
        moveAction.canceled -= ctx => movementDirection = ctx.ReadValue<Vector2>();
        jumpAction.started -= ctx => Jump();
    }
    private void Update()
    {
        if (!isActive) { return; }

        Move();

        currentState.UpdateState();
    }
    private void OnDestroy()
    {
        currentState.ExitState();
    }
    public void SwitchState(BasePlayerState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = newState;
        currentState.EnterState();
    }
    private void EventManager_OnRoundStart()
    {
        isActive = true;
        spriteRenderer.enabled = true;
        SwitchState(new PlayerIdleState(this));
    }
    private void EventManager_OnRoundComplete(bool obj)
    {
        anim.Rebind();
        isActive = false;
        spriteRenderer.enabled = false;
    }
    private void Move()
    {
        movementDirection.y = 0f;

        transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
    }
    private void Jump()
    {
        if (!IsGrounded()) { return; }

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    public bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position + leftRayPosition, transform.up, groundCheckDistance, groundLayerMask)
            || Physics2D.Raycast(transform.position + rightRayPosition, transform.up, groundCheckDistance, groundLayerMask);
    }
    private void SetSpriteDirection(float direction)
    {
        spriteRenderer.flipX = direction < 0f ? true : false;
        playerAttackController.SetColliderDirection(spriteRenderer.flipX);
    }
    public void PushPlayerInDirection(Vector2 position)
    {
        Vector2 direction = Vector2.zero;

        if (position.x > transform.position.x)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.right;
        }

        rb.AddForce(direction * pushForce, ForceMode2D.Impulse);
    }
}

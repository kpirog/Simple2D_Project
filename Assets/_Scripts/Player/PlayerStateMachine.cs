using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    [Header("Ground check settings")]
    [SerializeField] private float groundCheckDistance = -0.5f;
    [SerializeField] private Vector3 leftRayPosition;
    [SerializeField] private Vector3 rightRayPosition;
    [SerializeField] private LayerMask groundLayerMask;

    private BasePlayerState currentState;

    private Vector2 movementDirection;

    private Animator anim;
    private Rigidbody2D rb;

    private PlayerControls playerControls;
    private InputAction jumpAction;
    private InputAction moveAction;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
        moveAction.performed += ctx => movementDirection = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => movementDirection = ctx.ReadValue<Vector2>();
        jumpAction.started += ctx => Jump();
    }
    private void OnDisable()
    {
        playerControls.Disable();
        moveAction.performed -= ctx => movementDirection = ctx.ReadValue<Vector2>();
        moveAction.canceled -= ctx => movementDirection = ctx.ReadValue<Vector2>();
        jumpAction.started -= ctx => Jump();
    }
    private void Update()
    {
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
    private void Move()
    {
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
    }
    private void Jump()
    {
        if (!IsGrounded()) { return; }

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    private bool IsGrounded()
    {
        DrawRays();
        
        return Physics2D.Raycast(transform.position + leftRayPosition, transform.up, groundCheckDistance, groundLayerMask)
            || Physics2D.Raycast(transform.position + rightRayPosition, transform.up, groundCheckDistance, groundLayerMask);
    }
    private void DrawRays()
    {
        Debug.DrawRay(transform.position + rightRayPosition, transform.up * groundCheckDistance, Color.red);
        Debug.DrawRay(transform.position + leftRayPosition, transform.up * groundCheckDistance, Color.red);
    }
}

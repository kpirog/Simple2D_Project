using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    [Header("Patrol settings")]
    [SerializeField] private Vector2 patrolRange;
    [SerializeField] private float patrolRate;
    [SerializeField] private float minPatrolDistance = 1.0f;

    [Header("Attack settings")]
    [SerializeField] private float distanceToPlayer;

    [Header("Ground check settings")]
    [SerializeField] private float groundCheckDistance = -0.5f;
    [SerializeField] private Vector3 leftRayPosition;
    [SerializeField] private Vector3 rightRayPosition;
    [SerializeField] private LayerMask groundLayerMask;

    [Header("EnemyType")]
    [SerializeField] private EnemyType enemyType;

    private EnemyBaseState currentState;

    private EnemyHealth enemyHealth;
    private PlayerHealth player;
    private SpriteRenderer spriteRenderer;

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;

    public float PatrolRate => patrolRate;
    public float MinPatrolDistance => minPatrolDistance;
    public Vector2 PatrolRange => patrolRange;
    public Vector3 PlayerPosition => player.transform.position;
    public bool CanPatrol { get; set; }
    public bool IsAlive => enemyHealth.CurrentHealth > 0;
    public bool CanAttack
    {
        get
        {
            return player.CurrentHealth > 0 &&
                Vector2.Distance(player.transform.position, transform.position) <= distanceToPlayer &&
                enemyType == EnemyType.Angry;
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Start()
    {
        SwitchState(new EnemyIdleState(this));
    }
    private void Update()
    {
        //Debug.Log(IsGrounded());
        
        currentState.UpdateState();
    }
    private void OnDestroy()
    {
        currentState.ExitState();
    }
    public void SwitchState(EnemyBaseState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = newState;
        currentState.EnterState();
    }
    public void SetSpriteDirection(float direction)
    {
        Debug.Log("direction = " + direction);
        spriteRenderer.flipX = direction < 0f ? true : false;
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    public bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position + leftRayPosition, transform.up, groundCheckDistance, groundLayerMask)
            || Physics2D.Raycast(transform.position + rightRayPosition, transform.up, groundCheckDistance, groundLayerMask);
    }
}

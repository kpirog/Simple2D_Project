using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemyStateMachine : MonoBehaviour
{
    [Header("Patrol settings")]
    [SerializeField] private Vector2 patrolRange;
    [SerializeField] private float patrolRate;
    [SerializeField] private float minPatrolDistance = 1.0f;

    [Header("Chase settings")]
    [SerializeField] private float distanceToChase;

    [Header("Attack settings")]
    [SerializeField] private float distanceToAttack;

    [Header("Ground check settings")]
    [SerializeField] private float groundCheckDistance = -0.5f;
    [SerializeField] private Vector3 leftRayPosition;
    [SerializeField] private Vector3 rightRayPosition;
    [SerializeField] private LayerMask groundLayerMask;

    [Header("EnemyType")]
    [SerializeField] private EnemyType enemyType;

    private EnemyBaseState currentState;
    private ObjectPool<EnemyStateMachine> enemiesPool;

    private PlayerHealth player;
    private SpriteRenderer spriteRenderer;

    [HideInInspector] public EnemyHealth enemyHealth;
    [HideInInspector] public EnemyWeapon enemyWeapon;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;

    public EnemyType EnemyType => enemyType;
    public float PatrolRate => patrolRate;
    public float MinPatrolDistance => minPatrolDistance;
    public Vector2 PatrolRange => patrolRange;
    public Vector3 PlayerPosition => player.transform.position;
    public bool CanPatrol { get; set; }
    public bool IsAlive => enemyHealth.CurrentHealth > 0;
    public bool IsChasing
    {
        get
        {
            return player.CurrentHealth > 0 &&
                Vector2.Distance(player.transform.position, transform.position) <= distanceToChase &&
                Vector2.Distance(player.transform.position, transform.position) > distanceToAttack &&
                enemyType == EnemyType.Angry;
        }
    }
    public bool CanAttack
    {
        get
        {
            return player.CurrentHealth > 0 &&
                Vector2.Distance(player.transform.position, transform.position) <= distanceToAttack &&
                enemyType == EnemyType.Angry;
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyWeapon = GetComponent<EnemyWeapon>();
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
        if (enemyType == EnemyType.Angry && currentState is not EnemyAttackState)
        {
            enemyWeapon.DisableWeaponCollider();
        }

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
        spriteRenderer.flipX = direction < 0f ? true : false;

        if (enemyWeapon != null)
            enemyWeapon.SetColliderDirection(spriteRenderer.flipX);
    }
    public void ReleaseEnemy()
    {
        enemiesPool.Release(this);
    }
    public bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position + leftRayPosition, transform.up, groundCheckDistance, groundLayerMask)
            || Physics2D.Raycast(transform.position + rightRayPosition, transform.up, groundCheckDistance, groundLayerMask);
    }
    public void SetPool(ObjectPool<EnemyStateMachine> pool)
    {
        enemiesPool = pool;
    }
}

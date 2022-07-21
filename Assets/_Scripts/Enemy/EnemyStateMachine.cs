using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    [Header("Patrol settings")]
    [SerializeField] private Vector2 patrolRange;
    [SerializeField] private float patrolRate;

    private EnemyBaseState currentState;
    private PlayerHealth player;
    
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator anim;

    public float PatrolRate => patrolRate;
    public Vector2 PatrolRange => patrolRange;
    public bool CanPatrol { get; set; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerHealth>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Start()
    {
        SwitchState(new EnemyIdleState(this));
    }
    private void Update()
    {
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
}

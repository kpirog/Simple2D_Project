using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    private EnemyBaseState currentState;

    private NavMeshAgent agent;
    private PlayerHealth player;

    [HideInInspector] public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerHealth>();
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

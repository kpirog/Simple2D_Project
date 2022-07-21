using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private EnemyBaseState currentState;

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

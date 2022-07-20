using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private BasePlayerState currentState;

    private void Start()
    {
        SwitchState(new PlayerIdleState(this));
    }
    private void Update()
    {
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
}

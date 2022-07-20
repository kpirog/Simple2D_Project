using UnityEngine;

public abstract class BasePlayerState
{
    protected PlayerStateMachine playerStateMachine;

    public BasePlayerState(PlayerStateMachine playerStateMachine)
    {
        this.playerStateMachine = playerStateMachine;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}

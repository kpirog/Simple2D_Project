using UnityEngine;

public class PlayerMoveState : BasePlayerState
{
    public PlayerMoveState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Move State");
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        
    }
}

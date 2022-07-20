using UnityEngine;

public class PlayerAttackState : BasePlayerState
{
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Attack State");
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }
}

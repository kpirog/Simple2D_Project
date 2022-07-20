using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BasePlayerState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }
    
    public override void EnterState()
    {
        Debug.Log("Idle State");
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        
    }
}

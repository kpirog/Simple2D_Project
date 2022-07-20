using UnityEngine;

public class PlayerIdleState : BasePlayerState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }
    public override void EnterState()
    {
        
    }
    public override void ExitState()
    {
        
    }
    public override void UpdateState()
    {
        if (playerStateMachine.MovementDirection != Vector2.zero)
        {
            playerStateMachine.SwitchState(new PlayerMoveState(playerStateMachine));
        }
        else if (playerStateMachine.IsJumping)
        {
            playerStateMachine.SwitchState(new PlayerJumpState(playerStateMachine));
        }
        else if (playerStateMachine.IsFallingDown)
        {
            playerStateMachine.SwitchState(new PlayerFallDownState(playerStateMachine));
        }
    }
}

using UnityEngine;

public class PlayerMoveState : BasePlayerState
{
    private const string MoveAnimationKey = "IsMoving";

    public PlayerMoveState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }
    public override void EnterState()
    {
        playerStateMachine.anim.SetBool(MoveAnimationKey, true);
    }

    public override void ExitState()
    {
        playerStateMachine.anim.SetBool(MoveAnimationKey, false);
    }
    public override void UpdateState()
    {
        if (!playerStateMachine.IsAlive) { playerStateMachine.SwitchState(new PlayerDeathState(playerStateMachine)); }

        if (playerStateMachine.MovementDirection == Vector2.zero)
        {
            playerStateMachine.SwitchState(new PlayerIdleState(playerStateMachine));
        }
        else if (playerStateMachine.IsJumping)
        {
            playerStateMachine.SwitchState(new PlayerJumpState(playerStateMachine));
        }
    }
}

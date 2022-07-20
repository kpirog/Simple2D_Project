using UnityEngine;

public class PlayerJumpState : BasePlayerState
{
    private const string JumpAnimationKey = "IsJumping";

    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }
    public override void EnterState()
    {
        playerStateMachine.anim.SetBool(JumpAnimationKey, true);
    }

    public override void ExitState()
    {
        playerStateMachine.anim.SetBool(JumpAnimationKey, false);
    }

    public override void UpdateState()
    {
        if (playerStateMachine.IsFallingDown)
        {
            playerStateMachine.SwitchState(new PlayerFallDownState(playerStateMachine));
        }
    }
}

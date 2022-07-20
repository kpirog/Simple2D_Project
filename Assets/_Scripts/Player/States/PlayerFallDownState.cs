using UnityEngine;

public class PlayerFallDownState : BasePlayerState
{
    private const string FallDownAnimationKey = "IsFallingDown";

    public PlayerFallDownState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }
    public override void EnterState()
    {
        playerStateMachine.anim.SetBool(FallDownAnimationKey, true);
    }

    public override void ExitState()
    {
        playerStateMachine.anim.SetBool(FallDownAnimationKey, false);
    }

    public override void UpdateState()
    {
        if (playerStateMachine.IsGrounded())
        {
            playerStateMachine.SwitchState(new PlayerIdleState(playerStateMachine));
        }
    }
}

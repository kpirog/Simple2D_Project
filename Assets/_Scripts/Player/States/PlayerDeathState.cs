using UnityEngine;

public class PlayerDeathState : BasePlayerState
{
    private const string DeathAnimationKey = "Death";
    
    public PlayerDeathState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void EnterState()
    {
        playerStateMachine.anim.SetTrigger(DeathAnimationKey);
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        if (playerStateMachine.anim.GetCurrentAnimatorStateInfo(0).IsName(DeathAnimationKey))
        {
            playerStateMachine.DestroyPlayer();
        }
    }
}

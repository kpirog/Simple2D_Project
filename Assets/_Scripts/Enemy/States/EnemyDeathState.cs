using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    private const string DeathAnimationKey = "Death";

    public EnemyDeathState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        enemyStateMachine.anim.SetTrigger(DeathAnimationKey);
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        if (enemyStateMachine.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f && !enemyStateMachine.anim.IsInTransition(0))
        {
            enemyStateMachine.ReleaseEnemy();
        }
    }
}

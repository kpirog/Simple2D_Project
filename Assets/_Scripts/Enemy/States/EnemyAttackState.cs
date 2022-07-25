using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
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
        if (!enemyStateMachine.IsAlive)
        {
            enemyStateMachine.SwitchState(new EnemyDeathState(enemyStateMachine));
        }
        else
        {
            if (enemyStateMachine.CanAttack)
            {
                enemyStateMachine.agent.SetDestination(enemyStateMachine.PlayerPosition);
            }
            else
            {
                enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
            }
        }
    }
}

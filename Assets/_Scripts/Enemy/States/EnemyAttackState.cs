using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private int attackAnimKey = Animator.StringToHash("IsAttacking");

    public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }
    public override void EnterState()
    {
        if (!enemyStateMachine.agent.isStopped)
            enemyStateMachine.agent.isStopped = true;

        enemyStateMachine.anim.SetBool(attackAnimKey, true);
    }
    public override void ExitState()
    {
        enemyStateMachine.anim.SetBool(attackAnimKey, false);
    }
    public override void UpdateState()
    {
        if (!enemyStateMachine.IsAlive)
        {
            enemyStateMachine.SwitchState(new EnemyDeathState(enemyStateMachine));
        }
        else
        {
            if (!enemyStateMachine.CanAttack && enemyStateMachine.IsChasing)
            {
                enemyStateMachine.SwitchState(new EnemyChaseState(enemyStateMachine));
            }
            else if(!enemyStateMachine.CanAttack && !enemyStateMachine.IsChasing)
            {
                enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
            }
        }
    }
}

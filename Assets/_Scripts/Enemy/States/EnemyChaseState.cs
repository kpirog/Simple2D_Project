using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private int walkingAnimKey = Animator.StringToHash("IsWalking");

    public EnemyChaseState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }
    public override void EnterState()
    {
        if (enemyStateMachine.agent.isStopped)
            enemyStateMachine.agent.isStopped = false;

        enemyStateMachine.anim.SetBool(walkingAnimKey, true);
    }
    public override void ExitState()
    {
        enemyStateMachine.anim.SetBool(walkingAnimKey, false);
    }
    public override void UpdateState()
    {
        if (!enemyStateMachine.IsAlive)
        {
            enemyStateMachine.SwitchState(new EnemyDeathState(enemyStateMachine));
        }
        else
        {
            if (!enemyStateMachine.IsChasing && enemyStateMachine.CanAttack)
            {
                enemyStateMachine.SwitchState(new EnemyAttackState(enemyStateMachine));
            }
            else if (!enemyStateMachine.IsChasing && !enemyStateMachine.CanAttack)
            {
                enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
            }

            float directionToPlayer = enemyStateMachine.PlayerPosition.x > enemyStateMachine.transform.position.x ? 1f : -1f;

            enemyStateMachine.SetSpriteDirection(directionToPlayer);
            enemyStateMachine.agent.SetDestination(enemyStateMachine.PlayerPosition);
        }
    }
}

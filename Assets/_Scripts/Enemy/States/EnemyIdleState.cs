using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private float patrolTimer;

    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        patrolTimer = enemyStateMachine.PatrolRate;
        enemyStateMachine.CanPatrol = false;
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
            if (enemyStateMachine.IsChasing)
            {
                enemyStateMachine.SwitchState(new EnemyChaseState(enemyStateMachine));
            }
            else if (enemyStateMachine.CanPatrol)
            {
                enemyStateMachine.SwitchState(new EnemyPatrolState(enemyStateMachine));
            }
            else
            {
                SetPatrolTimer();
            }
        }
    }
    private void SetPatrolTimer()
    {
        patrolTimer -= Time.deltaTime;

        if (patrolTimer <= 0) { enemyStateMachine.CanPatrol = true; }
    }
}

using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private Vector2 patrolDestination;

    public EnemyPatrolState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }
    public override void EnterState()
    {
        Debug.Log("Patrol state");
        patrolDestination = GetRandomPatrolPosition();
    }
    public override void ExitState()
    {

    }
    public override void UpdateState()
    {
        if (!enemyStateMachine.CanPatrol)
        {
            enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
        }
        else
        {
            SetDestination();
        }
    }
    private Vector2 GetRandomPatrolPosition()
    {
        return new Vector2
        (
            x: Random.Range(enemyStateMachine.PatrolRange.x, enemyStateMachine.PatrolRange.y),
            y: enemyStateMachine.transform.position.y
        );
    }
    private void SetDestination()
    {
        if ((Vector2)enemyStateMachine.transform.position == patrolDestination)
        {
            enemyStateMachine.CanPatrol = false;
            return;
        }

        Debug.Log("Idzie");
        enemyStateMachine.agent.SetDestination(patrolDestination);
    }
}

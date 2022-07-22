using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private Vector2 patrolDestination;
    private int walkingAnimKey = Animator.StringToHash("IsWalking");

    public EnemyPatrolState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }
    public override void EnterState()
    {
        Debug.Log("Patrol state");
        enemyStateMachine.anim.SetBool(walkingAnimKey, true);
        patrolDestination = GetRandomPatrolPosition();
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

        enemyStateMachine.SetSpriteDirection(patrolDestination.x);
        enemyStateMachine.agent.SetDestination(patrolDestination);
    }
}

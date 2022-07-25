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
        enemyStateMachine.agent.isStopped = false;
        enemyStateMachine.anim.SetBool(walkingAnimKey, true);

        patrolDestination = new Vector2(GetRandomPatrolXPos(), enemyStateMachine.rb.velocity.y);
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
            if (enemyStateMachine.CanAttack)
            {
                enemyStateMachine.SwitchState(new EnemyAttackState(enemyStateMachine));
            }
            else if (!enemyStateMachine.CanPatrol)
            {
                enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
            }
            else if(enemyStateMachine.CanPatrol)
            {
                SetDestination();
            }
        }
    }
    private float GetRandomPatrolXPos()
    {
        float minDistance;
        float maxDistance;

        SetPatrolDistance(out minDistance, out maxDistance);

        return Random.Range(minDistance, maxDistance);
    }
    private void SetPatrolDistance(out float minDistance, out float maxDistance)
    {
        float currentXPos = enemyStateMachine.transform.position.x;
        int direction = SetPatrolDirection(currentXPos);

        maxDistance = direction > 0 ? enemyStateMachine.PatrolRange.y : enemyStateMachine.PatrolRange.x;
        minDistance = direction > 0 ? currentXPos + enemyStateMachine.MinPatrolDistance : currentXPos - enemyStateMachine.MinPatrolDistance;

        if (((minDistance >= maxDistance) && direction > 0) || ((minDistance <= maxDistance) && direction < 0))
        {
            maxDistance = -maxDistance;
        }

        enemyStateMachine.SetSpriteDirection(maxDistance);
    }
    private int SetPatrolDirection(float currentXPos)
    {
        int direction;

        if (currentXPos <= enemyStateMachine.PatrolRange.x)
        {
            direction = 1;
        }
        else if (currentXPos >= enemyStateMachine.PatrolRange.y)
        {
            direction = -1;
        }
        else
        {
            direction = Random.value > 0 ? 1 : -1;
        }

        return direction;
    }
    private void SetDestination()
    {
        if (HasEnemyReached())
        {
            enemyStateMachine.CanPatrol = false;
            enemyStateMachine.agent.isStopped = true;
            return;
        }

        if (enemyStateMachine.IsGrounded())
            patrolDestination.y = enemyStateMachine.rb.velocity.y;
        else
            patrolDestination.y = 0f;

        enemyStateMachine.agent.SetDestination(patrolDestination);
    }
    private bool HasEnemyReached()
    {
        float roundedXPos = Mathf.Round(enemyStateMachine.transform.position.x * 100f) / 100f;
        float roundedDestination = Mathf.Round(patrolDestination.x * 100f) / 100f;

        if (roundedXPos == roundedDestination && enemyStateMachine.IsGrounded())
        {
            return true;
        }

        return false;
    }
}

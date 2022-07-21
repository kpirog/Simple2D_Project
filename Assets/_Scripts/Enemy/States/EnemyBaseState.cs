public abstract class EnemyBaseState 
{
    protected EnemyStateMachine enemyStateMachine;

    protected EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        this.enemyStateMachine = enemyStateMachine;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}

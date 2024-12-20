using System.Collections;
using UnityEngine;

public abstract class EnemyState : IState
{
    protected Enemy _enemyController;

    public EnemyState(Enemy enemyController)
    {
        _enemyController = enemyController;
    }

    public void OnStateEnter()
    {
        OnEnter();
    }

    protected virtual void OnEnter(){}

    public void OnStateUpdate()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate(){}

    public IState OnStateProcessState()
    {
        return OnProcessState();
    }
    protected virtual IState OnProcessState()
    {
        return this;
    }

    public void OnStateExit()
    {
        OnExit();
    }

    protected virtual void OnExit(){}
}

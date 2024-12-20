using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowState : EnemyState
{
    private readonly INavigationHandler _navigationHandler;
    private readonly IEnemyBarricadeHandler _barricadeHandler;
    private readonly IEnemyMovementHandler _movementHandler;
    private readonly IEnemyAttackHandler _attackHandler;

    public FollowState(Enemy enemyController, INavigationHandler navigationHandler, IEnemyBarricadeHandler barricadeHandler, IEnemyMovementHandler movementHandler, IEnemyAttackHandler attackHandler) :
    base(enemyController)
    {
        _navigationHandler = navigationHandler;
        _barricadeHandler = barricadeHandler;
        _movementHandler = movementHandler;
        _attackHandler = attackHandler; 
    }

    protected override void OnEnter()
    {
        _navigationHandler.Resume();
    }

    protected override void OnUpdate()
    {
        _movementHandler.UpdateMovement();

        _barricadeHandler.OnUpdateBarricade();

        _navigationHandler.UpdateFollowing();
    }

    protected bool ShouldChangeToAttack()
    {
        if (_navigationHandler.CheckIfTargetInDistance(_movementHandler.EnemyTransform.position, _attackHandler.MinDistanceToStartAttack))
            return true;

        return false;
    }

    protected override IState OnProcessState()
    {
        if (ShouldChangeToAttack())
            return _enemyController.AttackState;

        return this;
    }

    protected override void OnExit()
    {
        _navigationHandler.Stop();
    }
}

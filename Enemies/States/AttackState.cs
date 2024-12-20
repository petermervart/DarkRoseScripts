using System.Collections;
using UnityEngine;

public class AttackState : EnemyState
{
    private INavigationHandler _navigationHandler;
    private IAnimationHandler _animationHandler;
    private IEnemyMovementHandler _movementHandler;
    private IEnemyAttackHandler _attackHandler;

    public AttackState(Enemy enemyController, IAnimationHandler animationHandler, INavigationHandler navigationHandler, IEnemyMovementHandler enemyMovementHandler, IEnemyAttackHandler enemyAttackHandler) :
    base(enemyController)
    {
        _navigationHandler = navigationHandler;
        _animationHandler = animationHandler;
        _movementHandler = enemyMovementHandler;
        _attackHandler = enemyAttackHandler;
    }

    protected override void OnEnter()
    {
        _animationHandler.SetBool("isattacking", true);
    }

    protected override IState OnProcessState()
    {
        if (ShouldChangeToFollow())
            return _enemyController.FollowState;

        return this;
    }

    protected bool ShouldChangeToFollow()
    {
        if (!_navigationHandler.CheckIfTargetInDistance(_movementHandler.EnemyTransform.position, _attackHandler.MinDistanceToStartAttack))
            return true;

        return false;
    }

    protected override void OnUpdate()
    {
        _navigationHandler.FaceTarget();
    }

    protected override void OnExit()
    {
        _animationHandler.SetBool("isattacking", false);
    }
}

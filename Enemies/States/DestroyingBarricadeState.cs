using System.Collections;
using UnityEngine;

public class DestroyingBarricadeState : EnemyState
{
    private IAnimationHandler _animationHandler;
    private IEnemyBarricadeHandler _barricadeHandler;
    private INavigationHandler _navigationHandler;
    private IEnemyMovementHandler _movementHandler;
    private IEnemyAttackHandler _attackHandler;

    private bool _isBarricadeDestroyed;

    public DestroyingBarricadeState(Enemy enemyController, IAnimationHandler animationHandler, IEnemyBarricadeHandler barricadeHandler, INavigationHandler navigationHandler, IEnemyMovementHandler movementHandler) :
    base(enemyController)
    {
        _animationHandler = animationHandler;
        _barricadeHandler = barricadeHandler;
        _navigationHandler = navigationHandler;
        _movementHandler = movementHandler;
    }

    protected override void OnEnter()
    {
        _animationHandler.SetBool("isattacking", true);

        _barricadeHandler.OnObstructionDestroyed += HandleBarricadeDestroyed;

        _isBarricadeDestroyed = false;
    }

    private void HandleBarricadeDestroyed()
    {
        _isBarricadeDestroyed = true;
    }

    protected bool ShouldChangeToAttack()
    {
        if (_navigationHandler.CheckIfTargetInDistance(_movementHandler.EnemyTransform.position, _attackHandler.MinDistanceToStartAttack) && !_isBarricadeDestroyed)
            return true;

        return false;
    }

    protected bool ShouldChangeToFollow()
    {
        if (!_isBarricadeDestroyed)
            return true;

        return false;
    }

    protected override IState OnProcessState()
    {
        if (ShouldChangeToAttack())
            return _enemyController.AttackState;

        if (ShouldChangeToFollow())
            return _enemyController.FollowState;

        return this;
    }

    protected override void OnExit()
    {
        _animationHandler.SetBool("isattacking", false);

        _barricadeHandler.OnObstructionDestroyed -= HandleBarricadeDestroyed;
    }

}

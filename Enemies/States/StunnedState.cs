using System.Collections;
using UnityEngine;

public class StunnedState : EnemyState
{
    private IAnimationHandler _animationHandler;
    private INavigationHandler _navigationHandler;
    private IEnemyStunHandler _stunHandler;
    private IEnemyMovementHandler _movementHandler;
    private IEnemyAttackHandler _attackHandler;

    private bool _isStunned;

    public StunnedState(Enemy enemyController, IAnimationHandler animationHandler, INavigationHandler navigationHandler, IEnemyStunHandler stunHandler, IEnemyMovementHandler movementHandler, IEnemyAttackHandler attackHandler) :
    base(enemyController)
    {
        _animationHandler = animationHandler;
        _navigationHandler = navigationHandler;
        _stunHandler = stunHandler;
        _movementHandler = movementHandler;
        _attackHandler = attackHandler;
    }

    protected override void OnEnter()
    {
        _isStunned = true;
        _animationHandler.SetBool("stunned", true);
        _stunHandler.OnStopStunned += HandleStopStun;
        _movementHandler.HandleCollider(false);
    }

    private void HandleStopStun()
    {
        _isStunned = false;
    }

    protected bool ShouldChangeToAttack()
    {
        if (_navigationHandler.CheckIfTargetInDistance(_movementHandler.EnemyTransform.position, _attackHandler.MinDistanceToStartAttack) && !_isStunned)
            return true;

        return false;
    }

    protected bool ShouldChangeToFollow()
    {
        if (!_isStunned)
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
        _stunHandler.OnStopStunned -= HandleStopStun;
        _animationHandler.SetBool("stunned", false);
        _movementHandler.HandleCollider(true);
    }

}

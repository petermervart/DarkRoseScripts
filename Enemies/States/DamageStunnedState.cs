using System.Collections;
using UnityEngine;

public class DamageStunnedState : EnemyState
{
    private readonly INavigationHandler _navigationHandler;
    private readonly IAnimationHandler _animationHandler;
    private readonly IEnemyHealth _healthHandler;
    private readonly IEnemyAttackHandler _attackHandler;
    private readonly IEnemyMovementHandler _movementHandler;

    private readonly Timer _timer;

    public DamageStunnedState(Enemy enemyController, IEnemyHealth healthHandler, IAnimationHandler animationHandler, INavigationHandler navigationHandler, IEnemyAttackHandler attackHandler, IEnemyMovementHandler movementHandler) :
    base(enemyController)
    {
        _navigationHandler = navigationHandler;
        _animationHandler = animationHandler;
        _healthHandler = healthHandler;
        _attackHandler = attackHandler;
        _movementHandler = movementHandler;

        _timer = new Timer(_healthHandler.TimeStunnedAfterAttack);
    }

    protected override void OnEnter()
    {
        _animationHandler.SetBool("gotHit", true);
        _navigationHandler.Stop();
        _timer.Reset();
        _timer.Start();
    }

    protected override IState OnProcessState()
    {
        if (ShouldChangeToAttack()) // first check for attack state and then check for follow to avoid checking distance to Target twice
            return _enemyController.AttackState;

        if (ShouldChangeToFollow())
            return _enemyController.FollowState;

        return this;
    }

    protected bool ShouldChangeToAttack()
    {
        if (_navigationHandler.CheckIfTargetInDistance(_movementHandler.EnemyTransform.position, _attackHandler.MinDistanceToStartAttack) && !_timer.IsRunning)
            return true;

        return false;
    }

    protected bool ShouldChangeToFollow()
    {
        if (!_timer.IsRunning)
            return true;

        return false;
    }

    protected override void OnUpdate()
    {
        _timer.Update(Time.deltaTime);
    }

    protected override void OnExit()
    {
        _animationHandler.SetBool("gotHit", false);
        _navigationHandler.Resume();
    }
}


using System.Collections;
using UnityEngine;

public class DeadState : EnemyState
{
    private readonly INavigationHandler _navigationHandler;
    private readonly IAnimationHandler _animationHandler;
    private readonly IEnemyHealth _healthHandler;

    private readonly Timer _timer;

    public DeadState(Enemy enemyController, IEnemyHealth healthHandler, IAnimationHandler animationHandler, INavigationHandler navigationHandler) :
    base(enemyController)
    {
        _navigationHandler = navigationHandler;
        _animationHandler = animationHandler;
        _healthHandler = healthHandler;

        _timer = new Timer(_healthHandler.TimeToDisappearAfterDeath);
    }

    protected override void OnEnter()
    {
        _animationHandler.SetTrigger("death");
        _navigationHandler.Stop();
        _timer.Reset();
        _timer.Start();
        _timer.OnTimerComplete += OnDisableObject;
    }

    private void OnDisableObject()
    {
        _enemyController.OnDisableObject();
    }

    protected override IState OnProcessState()
    {
        return this; // cant change to different state right now directly from this class, but might add resurrection and shit later...
    }

    protected override void OnUpdate()
    {
        _timer.Update(Time.deltaTime);
    }

    protected override void OnExit()
    {
        _timer.OnTimerComplete -= OnDisableObject;
    }
}


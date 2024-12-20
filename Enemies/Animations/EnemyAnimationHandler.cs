using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimationHandler : AnimationHandler, IEnemyAnimationHandler
{
    [Header("Running Animation Settings")]

    [SerializeField]
    private float _minRunningAnimationSpeed;

    [SerializeField]
    private float _maxRunningAnimationSpeed;

    [SerializeField]
    private string _runningAnimationModifierName;

    public void ChangeRunAnimationSpeed(float modifier)
    {
        _animator.SetFloat(_runningAnimationModifierName, Mathf.Clamp(modifier, _maxRunningAnimationSpeed, 1.0f));
    }
}
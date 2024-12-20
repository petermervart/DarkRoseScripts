using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementHandler : MonoBehaviour, IEnemyMovementHandler
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private NavMeshAgent _agent;

    [SerializeField]
    private float _stepInterval;

    [SerializeField]
    private float _maxStepInterval;

    [SerializeField]
    private float _groundTypeDistanceCheck;

    [SerializeField]
    private CapsuleCollider _collider;

    public Transform EnemyTransform => transform;

    public event Action<float> OnSpeedRatioChanged;

    public event Action<ESurfaceType> OnStep;

    private float _timePassedSinceLastFootStep;

    public void ResetMovement()
    {
        _timePassedSinceLastFootStep = 0f;
        _agent.speed = _speed;
        HandleCollider(true);
    }

    public void UpdateMovement()
    {
        OnSpeedRatioChanged?.Invoke(_agent.velocity.magnitude / _speed);
        HandleSteps();
    }

    public void HandleSpeedModifierChange(float modifier)
    {
        _agent.speed = _speed * modifier;
        _agent.velocity *= modifier; // slow down enemy instantly because otherwise they could go through the trap
    }

    private void HandleSteps()
    {
        _timePassedSinceLastFootStep += Time.deltaTime;

        float currentStepInterval = (_agent.velocity.magnitude / _speed) * _stepInterval;

        if (currentStepInterval > _maxStepInterval)
            currentStepInterval = _maxStepInterval;

        if (_timePassedSinceLastFootStep > currentStepInterval)
        {
            ESurfaceType groundMaterial = ESurfaceType.Ground;

            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitResult, _groundTypeDistanceCheck))
            {
                if (hitResult.collider.TryGetComponent<ISurfaceTypeHelper>(out var surface))
                    groundMaterial = surface.SurfaceType;
            }

            OnStep?.Invoke(groundMaterial);
            _timePassedSinceLastFootStep -= currentStepInterval;
        }
    }

    public void HandleCollider(bool isOn)
    {
        if (_collider != null)
        {
            _collider.enabled = isOn;
        }
    }
}
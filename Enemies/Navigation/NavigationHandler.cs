using System;
using UnityEngine;
using UnityEngine.AI;

public class NavigationHandler : MonoBehaviour, INavigationHandler
{
    [SerializeField]
    private NavMeshAgent _agent;

    [SerializeField]
    private float _pathUpdateRate;

    [SerializeField]
    private float _rotationSpeed;

    private Transform _target;

    private float _timer = 0f;

    protected float _timePassedSinceLastFootStep = 0f;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public Vector3 GetTargetPosition()
    {
        return _target.position;
    }

    public void UpdateFollowing()
    {
        _timer += Time.deltaTime;

        if (_agent.enabled && _agent.isOnNavMesh && _timer >= _pathUpdateRate)
        {
            _agent.SetDestination(_target.position);

            _timer = 0f;
        }
    }

    public void Resume()
    {
        _agent.enabled = true;

        if (_agent.isOnNavMesh)
            _agent.isStopped = false;
    }

    public void Stop()
    {
        if (_agent.enabled)
        {
            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;
            _agent.ResetPath();
            _agent.enabled = false;
        }
    }

    public bool CheckIfTargetInDistance(Vector3 position, float minDistance)
    {
        if (MathCalculations.GetDistance2D(position, _target.position) <= minDistance)
        {
            return true;
        }
        return false;
    }

    public void FaceTarget()
    {
        Vector3 lookDirection = _target.position - _agent.transform.position;
        lookDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}

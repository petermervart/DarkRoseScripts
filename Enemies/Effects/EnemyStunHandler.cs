using System;
using UnityEngine;

public class EnemyStunHandler : MonoBehaviour, IEnemyStunHandler
{
    private float _currentStunTime;

    private float _timeAlreadyStunned = 0f;

    public event Action OnStunned;

    public event Action OnStopStunned;

    public void ResetStun()
    {
        _timeAlreadyStunned = 0f;
        _currentStunTime = 0f;
    }

    public void OnEnemyStun(float stunTime)
    {
        if(_currentStunTime == 0)
            OnStunned?.Invoke();

        _currentStunTime += stunTime;
    }

    private void Update()
    {
        _timeAlreadyStunned += Time.deltaTime;

        if(_timeAlreadyStunned <= 0f)
        {
            OnStopStunned?.Invoke();
            _timeAlreadyStunned = 0f;
        }
    }
}
using System;
using UnityEngine;

public class Timer
{
    private float _duration;
    private float _elapsedTime;
    private bool _isRunning;

    public event Action OnTimerComplete;

    public bool IsRunning => _isRunning;

    public float TimeRemaining => Mathf.Max(_duration - _elapsedTime, 0f);

    public float Progress => Mathf.Clamp01(_elapsedTime / _duration);

    public Timer(float duration)
    {
        this._duration = duration;
        Reset();
    }

    public void Start()
    {
        _isRunning = true;
    }

    public void Pause()
    {
        _isRunning = false;
    }

    public void Reset()
    {
        _elapsedTime = 0f;
        _isRunning = false;
    }

    public void Update(float deltaTime)
    {
        if (!_isRunning)
            return;

        _elapsedTime += deltaTime;

        if (_elapsedTime >= _duration)
        {
            _isRunning = false;
            OnTimerComplete?.Invoke();
        }
    }

    public void SetDuration(float newDuration)
    {
        _duration = newDuration;
    }
}

using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public event Action OnGameEnded;
    public event Action OnGameStarted;
    public event Action<bool> OnGamePause;

    private bool _isGamePaused = false;

    public bool IsGamePaused => _isGamePaused;

    public void StartGame()
    {
        OnGameStarted?.Invoke();
    }

    public void ChangedPausedGame()
    {
        _isGamePaused = !_isGamePaused;
        StopTime(_isGamePaused);
        OnGamePause?.Invoke(_isGamePaused);
    }

    public void EndGame()
    {
        OnGameEnded?.Invoke();
    }

    public void StopTime(bool shouldStop)
    {
        if (shouldStop)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}

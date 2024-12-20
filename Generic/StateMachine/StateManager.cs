using UnityEngine;

public class StateManager : MonoBehaviour
{
    private IState _currentState;

    public void Initialize(EnemyState initialState)
    {
        ChangeState(initialState);
    }

    public bool CheckCurrentState(EnemyState stateToCheck)
    {
        return _currentState == stateToCheck;
    }

    public IState CurrentState
    {
        get => _currentState;
        private set => _currentState = value;
    }

    public void ChangeState(IState newState)
    {
        if (_currentState == newState)
            return;

        _currentState?.OnStateExit();
        _currentState = newState;
        _currentState?.OnStateEnter();
    }

    private void Update()
    {
        if (_currentState == null) return;

        IState nextState = _currentState.OnStateProcessState();
        if (_currentState != nextState)
        {
            ChangeState(nextState);
        }

        _currentState.OnStateUpdate();
    }
}
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInputHandler : BaseInputHandler
{
    public override EInputCategory Category => EInputCategory.Movement;

    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnLook;
    public event Action<bool> OnRun;

    private InputAction _move;
    private InputAction _look;
    private InputAction _run;

    protected override void BindInputActions()
    {
        _move = _playerInput.Player.Move;
        InitActionWithCanceled(_move, HandleMove);

        _look = _playerInput.Player.Look;
        InitActionWithCanceled(_look, HandleLook);

        _run = _playerInput.Player.Run;
        InitAction(_run, HandleRun);
    }

    protected override void UnbindInputActions()
    {
    }

    private void HandleMove(InputAction.CallbackContext ctx)
    {
        OnMove?.Invoke(ctx.ReadValue<Vector2>());
    }

    private void HandleLook(InputAction.CallbackContext ctx)
    {
        OnLook?.Invoke(ctx.ReadValue<Vector2>());
    }

    private void HandleRun(InputAction.CallbackContext ctx)
    {
        OnRun?.Invoke(ctx.control.IsPressed());
    }
}
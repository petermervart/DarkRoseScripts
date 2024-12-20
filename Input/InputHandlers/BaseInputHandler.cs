using System;
using UnityEngine.InputSystem;

public class BaseInputHandler : IInputHandler
{
    public virtual EInputCategory Category { get; protected set; }

    protected PlayerInputActions _playerInput;

    public virtual void Initialize(PlayerInputActions playerInput)
    {
        this._playerInput = playerInput;
        BindInputActions();
    }

    public virtual void Cleanup()
    {
        UnbindInputActions();
    }

    protected virtual void BindInputActions() { }

    protected virtual void InitAction(InputAction action, Action<InputAction.CallbackContext> callback)
    {
        action.Enable();

        action.performed += callback;
    }

    protected virtual void InitActionWithCanceled(InputAction action, Action<InputAction.CallbackContext> callback)
    {
        InitAction(action, callback);

        action.canceled += callback;
    }

    protected virtual void UnbindInputActions() { }
}

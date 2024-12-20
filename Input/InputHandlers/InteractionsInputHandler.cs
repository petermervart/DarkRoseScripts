using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionsInputHandler : BaseInputHandler
{
    public override EInputCategory Category => EInputCategory.Interacting;

    public event Action OnInteract;

    private InputAction _interact;

    protected override void BindInputActions()
    {
        _interact = _playerInput.Player.Interact;
        InitAction(_interact, HandleInteract);
    }

    protected override void UnbindInputActions()
    {

    }

    private void HandleInteract(InputAction.CallbackContext ctx)
    {
        OnInteract?.Invoke();
    }
}
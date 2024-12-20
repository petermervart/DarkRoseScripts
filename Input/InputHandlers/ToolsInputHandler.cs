using System;
using UnityEngine.InputSystem;

public class ToolsInputHandler : BaseInputHandler
{
    public override EInputCategory Category => EInputCategory.Tools;

    public event Action OnFlashlight;

    private InputAction _flashlight;

    protected override void BindInputActions()
    {
        _flashlight = _playerInput.Player.FlashLight;
        InitAction(_flashlight, HandleCraft);
    }

    protected override void UnbindInputActions()
    {

    }

    private void HandleCraft(InputAction.CallbackContext ctx)
    {
        OnFlashlight?.Invoke();
    }
}
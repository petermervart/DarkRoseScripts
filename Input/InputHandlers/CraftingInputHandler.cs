using System;
using UnityEngine.InputSystem;

public class CraftingInputHandler : BaseInputHandler
{
    public override EInputCategory Category => EInputCategory.Crafting;

    public event Action OnOpenCraft;

    private InputAction _openCraft;

    protected override void BindInputActions()
    {
        _openCraft = _playerInput.Player.OpenCraft;
        InitAction(_openCraft, HandleCraft);
    }

    protected override void UnbindInputActions()
    {

    }

    private void HandleCraft(InputAction.CallbackContext ctx)
    {
        OnOpenCraft?.Invoke();
    }
}
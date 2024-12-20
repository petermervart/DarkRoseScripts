using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingInputHandler : BaseInputHandler
{
    public override EInputCategory Category => EInputCategory.Building;

    public event Action OnOpenBuildChanged;
    public event Action OnClosedBuild;
    public event Action<int> OnChangedBuildIndex;
    public event Action OnPlacedBuild;

    private InputAction _build;

    private InputAction _closeBuild;

    private InputAction _placeBuild;

    protected override void BindInputActions()
    {
        _build = _playerInput.Player.Build;
        InitAction(_build, HandleBuild);

        _closeBuild = _playerInput.Player.CloseBuilding;
        InitAction(_closeBuild, HandleBuild);

        _placeBuild = _playerInput.Player.PlaceBuild;
        InitAction(_placeBuild, HandlePlacedBuild);
    }

    protected override void UnbindInputActions()
    {
        _build.performed -= HandleBuild;
    }

    private void HandleBuild(InputAction.CallbackContext ctx)
    {
        OnOpenBuildChanged?.Invoke();
    }

    private void HandleCloseBuild(InputAction.CallbackContext ctx)
    {
        OnClosedBuild?.Invoke();
    }

    private void HandlePlacedBuild(InputAction.CallbackContext ctx)
    {
        OnPlacedBuild?.Invoke();
    }
}
using System;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public event Action<bool> BuildingToggled;

    public event Action OnPlacingBuildable;

    private bool _isBuildingLocked = false;

    private IBuildingHandler[] _buildingHandlers;

    private void Awake()
    {
        _buildingHandlers = GetComponentsInChildren<IBuildingHandler>();

        for (int i = 0; i < _buildingHandlers.Length; i++)
        {
            _buildingHandlers[i].OnBuildingOpenChanged += HandleOpenedHandler;
            _buildingHandlers[i].OnBuildableChosen += BuildableChosen;
        }
    }

    public void SetLock(bool isLocked)
    {
        _isBuildingLocked = isLocked;

        for (int i = 0; i < _buildingHandlers.Length; i++)
        {
            _buildingHandlers[i].HandleLock(_isBuildingLocked);
        }
    }

    public void HandleOpenedHandler(bool isOpened)
    {
        BuildingToggled?.Invoke(isOpened);
    }

    public void BuildableChosen(BuildableItemConfigSO buildableConfig)
    {
        OnPlacingBuildable?.Invoke();
    }
}
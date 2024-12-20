using System;
using UnityEngine;
using AYellowpaper;

public class DefaultBuildingHandler : MonoBehaviour, IBuildingHandler
{
    [SerializeField]
    private BuildingHandlerConfigSO _buildingHandlerConfig;

    [SerializeField]
    private InterfaceReference<IInventoryHandler> _inventoryHandler;

    [SerializeField]
    private InterfaceReference<IBuildingUIHandler> _buildingUIHandler;

    [SerializeField]
    private CrosshairHelperUI _crosshairHelperUI;

    [SerializeField]
    private InputManager _inputManager;

    public BuildingHandlerConfigSO BuildingHandlerConfigSO { get { return _buildingHandlerConfig; } }

    public event Action<bool> OnBuildingOpenChanged;

    public event Action<BuildableItemConfigSO> OnBuildableChosen;

    public event Action<BuildableItemConfigSO> OnBuildablePlaced;

    private BuildingInputHandler _buildingInput;

    private BuildableItemConfigSO _currentBuildable;

    private bool _isBuildingOpened = false;

    private bool _isBuildingLocked = false;

    private void Start()
    {
        _buildingInput = _inputManager.GetInputHandler<BuildingInputHandler>(EInputCategory.Building);
        _buildingInput.OnOpenBuildChanged += HandleOpenBuildingChanged;

        _buildingUIHandler.Value.OnBuildableChanged += HandleBuildableChanged;

        _buildingUIHandler.Value.Initialize(_inventoryHandler.Value, this, _buildingHandlerConfig);
    }

    public void OnBuild(ITrapManager trapManager)
    {
        TimedTextHelperConfigSO helperText;

        if (!_inventoryHandler.Value.CheckItemAvailability(_currentBuildable.InventoryItemType, 1))
        {
            helperText = _buildingHandlerConfig.CantAffordTextHelper;
        }
        else
        {
            bool wasPlaced = trapManager.OnTrapPlaced(_currentBuildable.InventoryItemType.ItemID, out helperText);

            if (wasPlaced)
            {
                _inventoryHandler.Value.RemoveItemFromInventory(_currentBuildable.InventoryItemType, 1);
                OnBuildablePlaced?.Invoke(_currentBuildable);
            }
        }

        // could add string formatting here to add placeable item name...

        _crosshairHelperUI.SetTimedHelperText(helperText.Text, helperText.TextColor, helperText.DurationToShow);
    }

    public void HandleLock(bool isLocked)
    {
        _isBuildingLocked = isLocked;
    }

    public void HandleOpenBuildingChanged() // opened/closed from input
    {
        if (_isBuildingLocked)
            return;

        _isBuildingOpened = !_isBuildingOpened;
        OnBuildingOpenChanged.Invoke(_isBuildingOpened);
    }

    public void HandleBuildableChanged(BuildableItemConfigSO buildableConfig)
    {
        _currentBuildable = buildableConfig;
        _isBuildingOpened = false;
        OnBuildableChosen?.Invoke(buildableConfig);
    }
}

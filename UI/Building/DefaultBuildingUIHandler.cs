using UnityEngine;
using System.Collections.Generic;
using System;
using AYellowpaper;

public class DefaultBuildingUIHandler : SmoothUICanvasGroup, IBuildingUIHandler
{
    [SerializeField]
    private GameObject _buildableItemPrefab;

    [SerializeField]
    private Transform _buildableItemsParentTransform;

    [SerializeField]
    private InterfaceReference<IBuildableUIPlacerHandler> _buildablePlacer;

    public event Action<BuildableItemConfigSO> OnBuildableChanged;

    private IInventoryHandler _inventoryHandler;

    private IBuildingHandler _buildingHandler;

    private BuildingHandlerConfigSO _buildingHandlerConfig;

    private List<IBuildableItemUI> _buildableItemHolders;

    public void Initialize(IInventoryHandler inventoryHandler, IBuildingHandler buildingHandler, BuildingHandlerConfigSO buildingHandlerConfig)
    {
        _inventoryHandler = inventoryHandler;
        _buildingHandler = buildingHandler;

        _buildingHandler.OnBuildingOpenChanged += HandleBuildingeToggled;

        _buildingHandlerConfig = buildingHandlerConfig;

        _buildablePlacer.Value.Initialize(inventoryHandler, this);

        InitializeCraftableItems();
    }

    private void InitializeCraftableItems()
    {
        _buildableItemHolders = new List<IBuildableItemUI>();

        for (int i = 0; i < _buildingHandlerConfig.BuildableItems.Count; i++)
        {
            BuildableItemConfigSO currentItem = _buildingHandlerConfig.BuildableItems[i];

            GameObject buildableItemObject = Instantiate(_buildableItemPrefab, Vector3.zero, Quaternion.identity, _buildableItemsParentTransform);

            if (buildableItemObject.TryGetComponent(out IBuildableItemUI buildableItem))
            {
                _buildableItemHolders.Add(buildableItem);
                buildableItem.Initialize(currentItem, _inventoryHandler);

                buildableItem.OnChosenBuildable += HandleBuildableChanged;
            }
        }
    }

    private void HandleBuildableChanged(BuildableItemConfigSO buildableItem)
    {
        OnBuildableChanged?.Invoke(buildableItem);

        _buildablePlacer.Value.OnChoseBuildable(buildableItem);

        HandleBuildingeToggled(false);
    }

    private void HandleBuildingeToggled(bool isOpen)
    {
        if (isOpen)
        {
            Open();
            _buildablePlacer.Value.OnBuildingOpened();
        }
        else
        {
            Close();
        }
    }

    private void OnDestroy()
    {
        foreach (IBuildableItemUI buildableItem in _buildableItemHolders)
        {
            buildableItem.OnChosenBuildable -= HandleBuildableChanged;
        }
    }

}


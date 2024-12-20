using System;

public interface IBuildingUIHandler
{
    public event Action<BuildableItemConfigSO> OnBuildableChanged;

    public void Initialize(IInventoryHandler inventoryHandler, IBuildingHandler buildingHandler, BuildingHandlerConfigSO buildingHandlerConfig);
}
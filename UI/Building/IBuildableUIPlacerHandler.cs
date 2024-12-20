using System;

public interface IBuildableUIPlacerHandler
{
    public void Initialize(IInventoryHandler inventoryHandler, IBuildingUIHandler buildingHandler);

    public void OnChoseBuildable(BuildableItemConfigSO newBuildable);

    public void UpdateBuildable(BuildableItemConfigSO newBuildable);

    public void OnBuildingOpened();
}

using System;

public interface IBuildableItemUI
{
    public event Action<BuildableItemConfigSO> OnChosenBuildable;

    public void Initialize(BuildableItemConfigSO buildableItemConfig, IInventoryHandler inventoryHandler);
}

using System;

public interface ICraftableItemUI
{
    void Initialize(CraftableItemConfigSO craftableItemConfig, IInventoryHandler inventoryHandler);

    event Action<CraftableItemConfigSO, bool> OnCraftingStatusChanged;

    event Action<CraftableItemConfigSO> OnItemCrafted;
}

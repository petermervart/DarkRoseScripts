using System;

public interface ICraftingUIHandler
{
    public event Action<CraftableItemConfigSO, bool> OnCraftingStatusChanged;
    public event Action<CraftableItemConfigSO> OnItemCrafted;

    void Initialize(IInventoryHandler inventoryHandler, ICraftingHandler craftingHandler, CraftingHandlerConfigSO craftingHandlerConfig);
}

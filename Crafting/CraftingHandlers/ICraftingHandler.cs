using System;

public interface ICraftingHandler
{
    public CraftingHandlerConfigSO CraftingHandlerConfigSO { get; }

    public event Action<bool> OnCraftingOpenChanged;

    public event Action<CraftableItemConfigSO, bool> OnCraftingStatusChanged;

    public event Action<CraftableItemConfigSO> OnItemCrafted;

    void HandleLock(bool isLocked);

    bool CanCraftItem(CraftableItemConfigSO itemConfig);
    void CraftItem(CraftableItemConfigSO itemConfig);
}

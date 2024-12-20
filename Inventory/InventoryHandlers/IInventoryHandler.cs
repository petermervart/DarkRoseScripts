using System.Collections.Generic;
using System;

public enum EInvetoryHandlerType
{
    Default = 0,
}

public interface IInventoryHandler
{
    void SubscribeToItemEvent(Guid itemID, Action<int> action);

    void UnsubscribeToItemEvent(Guid itemID, Action<int> action);

    public void SetLock(bool isLocked);
    public bool CheckCraftingMaterialsAvailability(List<InventoryItemHolder> materials);

    public bool CheckCraftingMaterialAvailability(InventoryItemHolder item);

    public bool CheckItemAvailability(InventoryItemConfigSO item, int amount);

    public int AddItemToInventory(InventoryItemConfigSO item, int amount);

    public int RemoveItemFromInventory(InventoryItemConfigSO item, int amount);

    public void OnCraftedItem(CraftableItemConfigSO craftingItem);

    public int GetItemAmount(InventoryItemConfigSO item);
}

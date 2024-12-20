using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;

public class DefaultInventoryHandler : MonoBehaviour, IInventoryHandler
{
    [SerializeField]
    private InventoryHandlerConfigSO _configSO;

    private Dictionary<Guid, InventoryItemHolder> _itemDictionary;

    private Dictionary<Guid, Action<int>> _itemsUpdateEvents;

    private bool _isLocked = false;

    private void Awake()
    {
        InitializeInventoryItems();
    }

    private void Start()
    {
        // Invoke after first frame to make sure everything is set
        StartCoroutine(InvokeEventAfterFirstFrame());
    }

    private IEnumerator InvokeEventAfterFirstFrame()
    {
        yield return null;

        foreach (var key in _itemsUpdateEvents.Keys)
        {
            _itemsUpdateEvents[key].Invoke(_itemDictionary[key].ItemAmount);
        }
    }

    public void SubscribeToItemEvent(Guid itemID, Action<int> action)
    {
        _itemsUpdateEvents[itemID] += action;
    }

    public void UnsubscribeToItemEvent(Guid itemID, Action<int> action)
    {
        _itemsUpdateEvents[itemID] -= action;
    }

    public void SetLock(bool isLocked)
    {
        _isLocked = isLocked;
    }

    public void InitializeInventoryItems()
    {
        _itemDictionary = new Dictionary<Guid, InventoryItemHolder>();

        _itemsUpdateEvents = new Dictionary<Guid, Action<int>>();

        foreach (InventoryItemConfigSO inventoryItem in _configSO.StorableItems)
        {

            _itemDictionary.Add(inventoryItem.ItemID, new InventoryItemHolder(inventoryItem, 5)); // in the future this will be replaced with loading data from save system

            _itemsUpdateEvents.Add(inventoryItem.ItemID, _ => { });
        }


    }

    public bool CheckCraftingMaterialsAvailability(List<InventoryItemHolder> materials)
    {
        bool areItemsAvailable = true;
        foreach (InventoryItemHolder item in materials)
        {
            areItemsAvailable = areItemsAvailable && CheckCraftingMaterialAvailability(item);
        }

        return areItemsAvailable;
    }

    public bool CheckCraftingMaterialAvailability(InventoryItemHolder item)
    {
        if (_itemDictionary.TryGetValue(item.InventoryItem.ItemID, out InventoryItemHolder inventoryItem))
        {
            return inventoryItem.CheckIfAvailable(item.ItemAmount);
        }

        return false;
    }

    public bool CheckItemAvailability(InventoryItemConfigSO item, int amount)
    {
        if (_itemDictionary.TryGetValue(item.ItemID, out InventoryItemHolder inventoryItem))
        {
            return inventoryItem.CheckIfAvailable(amount);
        }

        return false;
    }

    public int AddItemToInventory(InventoryItemConfigSO item, int amount)
    {
        if (_itemDictionary.TryGetValue(item.ItemID, out InventoryItemHolder inventoryItem))
        {
            inventoryItem.AddAmount(amount);

            DebugUtils.Log("Added " + amount.ToString() + " " + item.Name + " new amount = " + inventoryItem.ItemAmount);

            _itemsUpdateEvents[item.ItemID]?.Invoke(inventoryItem.ItemAmount);

            return inventoryItem.ItemAmount;
        }
        else
        {
            DebugUtils.LogWarning("Item with ID " + item.ItemID + " not found in the inventory.");
            return -1;
        }
    }

    public int RemoveItemFromInventory(InventoryItemConfigSO item, int amount)
    {
        if (_itemDictionary.TryGetValue(item.ItemID, out InventoryItemHolder inventoryItem))
        {
            inventoryItem.ReduceAmount(amount);

            DebugUtils.Log(item.ItemID.ToString() + " Removed " + amount.ToString() + " " + item.Name + " new amount = " + inventoryItem.ItemAmount);

            _itemsUpdateEvents[item.ItemID]?.Invoke(inventoryItem.ItemAmount);

            return inventoryItem.ItemAmount;
        }
        else
        {
            DebugUtils.LogWarning("Item with ID " + item.ItemID + " not found in the inventory.");
            return -1;
        }
    }

    public void OnCraftedItem(CraftableItemConfigSO craftingItem)
    {
        foreach (InventoryItemHolder item in craftingItem.MatsToCraftItem)
        {
            RemoveItemFromInventory(item.InventoryItem, item.ItemAmount);
        }

        AddItemToInventory(craftingItem.InventoryItemType, 1); // just crafting one at a time for now might change later...
    }

    public int GetItemAmount(InventoryItemConfigSO item)
    {
        if(_itemDictionary != null && _itemDictionary.TryGetValue(item.ItemID, out InventoryItemHolder inventoryItem))
        {
            return inventoryItem.ItemAmount;
        }
        else
        {
            return 0;
        }
    }
}
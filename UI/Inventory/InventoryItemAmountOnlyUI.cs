using UnityEngine;
using TMPro;
using System;

public class InventoryItemAmountOnlyUI : MonoBehaviour, IInventoryItemUI
{
    [SerializeField]
    private TextMeshProUGUI _inventoryText;

    public event Action<bool> OnNoItemsChanged;

    private InventoryItemConfigSO _inventoryItem;

    private IInventoryHandler _inventoryHandler;

    private int _amount;

    public void Initialize(IInventoryHandler inventorHandler)
    {
        _inventoryHandler = inventorHandler;
    }

    public void ChangeItem(InventoryItemConfigSO inventoryItem)
    {
        _inventoryItem = inventoryItem;
        UpdateInventoryItem(inventoryItem);
    }

    public void UpdateInventoryItem(InventoryItemConfigSO newInventoryItem)
    {
        if(_inventoryHandler != null)
            _inventoryHandler.UnsubscribeToItemEvent(_inventoryItem.ItemID, UpdateAmount); // unsubsribe old inventory item

        _inventoryItem = newInventoryItem;

        UpdateAmount(_inventoryHandler.GetItemAmount(_inventoryItem));

        _inventoryHandler.SubscribeToItemEvent(_inventoryItem.ItemID, UpdateAmount);  // subsribe new inventory item
    }

    public void UpdateAmount(int newAmount)
    {
        _inventoryText.text = $"{newAmount}x";

        if (_amount != newAmount)
        {
            if (newAmount == 0 || _amount == 0)
            {
                OnNoItemsChanged?.Invoke(newAmount == 0);
            }

            _amount = newAmount;
        }
    }

    public void SetColor(Color color)
    {
        _inventoryText.color = color;
    }
}
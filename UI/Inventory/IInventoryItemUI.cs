using UnityEngine;
using System;

public interface IInventoryItemUI
{
    public event Action<bool> OnNoItemsChanged;

    public void Initialize(IInventoryHandler inventorHandler);

    public void ChangeItem(InventoryItemConfigSO inventoryItem);

    public void SetColor(Color color);

    public void UpdateAmount(int newAmount);
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class DefaultCraftingUIHandler : SmoothUICanvasGroup, ICraftingUIHandler
{
    [SerializeField]
    private GameObject _craftabeItemPrefab;

    [SerializeField]
    private Transform _craftabeItemsParentTransform;

    public event Action<CraftableItemConfigSO, bool> OnCraftingStatusChanged;
    public event Action<CraftableItemConfigSO> OnItemCrafted;

    private IInventoryHandler _inventoryHandler;

    private ICraftingHandler _craftingHandler;

    private CraftingHandlerConfigSO _craftingHandlerConfig;

    private List<ICraftableItemUI> _craftableItemHolders;

    public void Initialize(IInventoryHandler inventoryHandler, ICraftingHandler craftingHandler, CraftingHandlerConfigSO craftingHandlerConfig)
    {
        _inventoryHandler = inventoryHandler;
        _craftingHandler = craftingHandler;

        _craftingHandler.OnCraftingOpenChanged += HandleCraftingToggled;

        _craftingHandlerConfig = craftingHandlerConfig;
        
        InitializeCraftableItems();
    }

    private void InitializeCraftableItems()
    {
        _craftableItemHolders = new List<ICraftableItemUI>();

        for (int i = 0; i < _craftingHandlerConfig.CraftableItems.Count; i++)
        {
            CraftableItemConfigSO currentItem = _craftingHandlerConfig.CraftableItems[i];

            GameObject craftableItemObject = Instantiate(_craftabeItemPrefab, Vector3.zero, Quaternion.identity, _craftabeItemsParentTransform);

            if (craftableItemObject.TryGetComponent(out ICraftableItemUI craftableItem))
            {
                _craftableItemHolders.Add(craftableItem);
                craftableItem.Initialize(currentItem, _inventoryHandler);

                craftableItem.OnItemCrafted += HandleItemCrafted;
                craftableItem.OnCraftingStatusChanged += HandleCraftingStatusChanged;
            }
        }
    }

    private void HandleItemCrafted(CraftableItemConfigSO craftedItem)
    {
        OnItemCrafted?.Invoke(craftedItem);
    }

    private void HandleCraftingStatusChanged(CraftableItemConfigSO craftedItem, bool isCrafting)
    {
        OnCraftingStatusChanged?.Invoke(craftedItem, isCrafting);
    }

    private void HandleCraftingToggled(bool isOpen)
    {
        if (isOpen)
            Open();
        else
            Close();
    }

    private void OnDestroy()
    {
        foreach (var itemHolder in _craftableItemHolders)
        {
            // Unsubscribe from events
            itemHolder.OnItemCrafted -= HandleItemCrafted;
            itemHolder.OnCraftingStatusChanged -= HandleCraftingStatusChanged;
        }
    }

}

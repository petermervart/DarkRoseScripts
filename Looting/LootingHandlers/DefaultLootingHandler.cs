using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;

public class DefaultLootingHandler : MonoBehaviour, ILootingHandler
{
    [SerializeField]
    private CrosshairHelperUI _crosshairHelperUI;

    [SerializeField]
    private LootedItemsUIManager _lootedItemsUIManager;

    [SerializeField]
    private InterfaceReference<IInventoryHandler> _inventoryHandler;

    [SerializeField]
    private LootingUIBar _lootingBar;

    public event Action<LootableObjectConfigSO, bool> OnLootingStateChanged;

    public event Action<LootableObjectConfigSO> OnLootedItem;

    public event Action<float> OnLootingProgressChanged;

    private bool _isLooting = false;

    private bool _isLocked = false;

    public void HandleLock(bool isLocked)
    {
        _isLocked = isLocked;
    }

    public void LootedObject(ILootable lootableObject)
    {
        if (_isLocked || _isLooting)
            return;

        if (lootableObject.CanBeLooted)
        {
            StartCoroutine(LootDelayCoroutine(lootableObject));
        }
        else
        {
            TimedTextHelperConfigSO helperText = lootableObject.FailedTryingLoot();
            _crosshairHelperUI.SetTimedHelperText(helperText.Text, helperText.TextColor, helperText.DurationToShow);
        }
    }

    private void StartLooting(ILootable lootableObject)
    {
        _isLooting = true;
        OnLootingStateChanged?.Invoke(lootableObject.LootableObjectConfig, true);
        _lootingBar.HandleOpenChanged(true);
    }

    private void StopLooting(ILootable lootableObject)
    {
        _isLooting = false;
        OnLootingStateChanged?.Invoke(lootableObject.LootableObjectConfig, false);
        _lootingBar.HandleOpenChanged(false);
    }

    private IEnumerator LootDelayCoroutine(ILootable lootableObject)
    {
        float elapsedTime = 0f;

        StartLooting(lootableObject);

        while (elapsedTime < lootableObject.LootingTime)
        {
            float currentProgress = Mathf.Clamp01(elapsedTime / lootableObject.LootingTime);

            OnLootingProgressChanged?.Invoke(currentProgress);
            _lootingBar.UpdateLootingProgress(currentProgress);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        LootItems(lootableObject);
    }

    private void LootItems(ILootable lootableObject)
    {
        List<InventoryItemHolder> lootedItems = lootableObject.GenerateLoot();

        if (lootedItems.Count <= 0)
        {
            StopLooting(lootableObject);

            // could play a different sound when empty in the future, right now no sound

            TimedTextHelperConfigSO helperText = lootableObject.ObjectWasEmpty();
            _crosshairHelperUI.SetTimedHelperText(helperText.Text, helperText.TextColor, helperText.DurationToShow);
            return;
        }

        foreach (InventoryItemHolder item in lootedItems)
        {
            int newAmount = _inventoryHandler.Value.AddItemToInventory(item.InventoryItem, item.ItemAmount);

            if (newAmount == -1)
                continue;

            string lootText = "+ " + item.ItemAmount.ToString() + " (" + newAmount.ToString() + ")";
            Sprite icon = item.InventoryItem.IconSprite;

            _lootedItemsUIManager.EnqueueLoot(lootText, icon);
        }

        StopLooting(lootableObject);

        OnLootedItem?.Invoke(lootableObject.LootableObjectConfig);
    }

}

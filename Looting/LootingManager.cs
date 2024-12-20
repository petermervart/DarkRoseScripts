using System;
using UnityEngine;

public class LootingManager : MonoBehaviour
{
    public event Action<bool> LootingToggled;

    private bool _isLootingLocked = false;

    private ILootingHandler[] _lootingHandlers;

    private void Awake()
    {
        _lootingHandlers = GetComponentsInChildren<ILootingHandler>();

        for (int i = 0; i < _lootingHandlers.Length; i++)
        {
            _lootingHandlers[i].OnLootingStateChanged += HandleOpenedHandler;
        }
    }

    public void SetLock(bool isLocked)
    {
        _isLootingLocked = isLocked;

        for (int i = 0; i < _lootingHandlers.Length; i++)
        {
            _lootingHandlers[i].HandleLock(_isLootingLocked);
        }
    }

    public void HandleOpenedHandler(LootableObjectConfigSO lootedItem, bool isOpened)
    {
        LootingToggled?.Invoke(isOpened);
    }
}
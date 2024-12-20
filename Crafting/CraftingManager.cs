using System;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public event Action<bool> CraftingToggled;

    private bool _isCraftingLocked = false;

    private ICraftingHandler[] _craftingHandlers;

    private void Awake()
    {
        _craftingHandlers = GetComponentsInChildren<ICraftingHandler>();

        for (int i = 0; i < _craftingHandlers.Length; i++)
        {
            _craftingHandlers[i].OnCraftingOpenChanged += HandleOpenedHandler;
        }
    }

    public void SetLock(bool isLocked)
    {
        _isCraftingLocked = isLocked;

        for(int i = 0; i < _craftingHandlers.Length; i++)
        {
            _craftingHandlers[i].HandleLock(_isCraftingLocked);
        }
    }

    public void HandleOpenedHandler(bool isOpened)
    {
        CraftingToggled?.Invoke(isOpened);
    }
}
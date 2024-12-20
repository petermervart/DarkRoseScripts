using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private IInventoryHandler[] _inventoryHandlers;

    private bool _isInventoryLocked = false;

    private void Awake()
    {
        _inventoryHandlers = GetComponentsInChildren<IInventoryHandler>(); // get all inventory handlers. Could be better but want to avoid using Singleton or Service Locator. Not a big fan of DI in Unity
    }

    public void SetInventoryLock(bool isLocked)
    {
        _isInventoryLocked = isLocked;

        for(int i = 0; i < _inventoryHandlers.Length; i++)
        {
            _inventoryHandlers[i].SetLock(isLocked);
        }

    }
}
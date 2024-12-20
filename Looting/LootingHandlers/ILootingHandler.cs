using System;

public interface ILootingHandler
{
    public event Action<LootableObjectConfigSO, bool> OnLootingStateChanged;
    public event Action<LootableObjectConfigSO> OnLootedItem;
    public event Action<float> OnLootingProgressChanged;

    void HandleLock(bool isLocked);
    void LootedObject(ILootable lootableObject);
}
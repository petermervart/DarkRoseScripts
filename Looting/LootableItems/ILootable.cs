using System.Collections.Generic;

public interface ILootable : IInteractable
{
    public float LootingTime { get; }
    public bool CanBeLooted { get; }
    public LootableObjectConfigSO LootableObjectConfig { get; }

    TimedTextHelperConfigSO FailedTryingLoot();
    public TimedTextHelperConfigSO ObjectWasEmpty();
    List<InventoryItemHolder> GenerateLoot();
}

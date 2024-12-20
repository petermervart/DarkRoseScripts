using System;

public interface ITrap
{
    public InventoryItemConfigSO InventoryItem { get; }

    public event Action OnTrapRemoved;

    public void Placed();
}

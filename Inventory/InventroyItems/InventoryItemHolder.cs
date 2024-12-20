
[System.Serializable]
public class InventoryItemHolder
{
    public InventoryItemConfigSO InventoryItem;
    public int ItemAmount;

    public InventoryItemHolder(InventoryItemConfigSO inventoryItem, int itemAmount = 0)
    {
        InventoryItem = inventoryItem;
        ItemAmount = itemAmount;
    }

    public void AddAmount(int amount)
    {
        ItemAmount += amount;
    }

    public void ReduceAmount(int amount)
    {
        ItemAmount -= amount;
    }

    public bool CheckIfAvailable(int amount)
    {
        if (amount <= ItemAmount)
            return true;

        return false;
    }
}
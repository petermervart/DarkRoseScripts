
internal interface IAudioCraftingHandler
{
    public void HandleItemCrafted(CraftableItemConfigSO craftedItem);

    public void HandleItemBeingCrafted(CraftableItemConfigSO craftedItem, bool isCrafting);
}

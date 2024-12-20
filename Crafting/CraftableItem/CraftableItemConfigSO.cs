using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Craftable Item Config", fileName = "CraftableItemConfig")]
public class CraftableItemConfigSO : ScriptableObject
{
    [Header("Crafting")]
    public List<InventoryItemHolder> MatsToCraftItem = new List<InventoryItemHolder>();
    public InventoryItemConfigSO InventoryItemType;
    public float CraftingTime;
    public float AfterCraftedDelay;

    [Header("Audio")]
    public CraftableItemAudioConfigSO AudioConfig;

    [Header("UI")]
    public CraftableItemUIConfigSO UIConfig;
}

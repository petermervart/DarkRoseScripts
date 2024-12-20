using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Crafting Handler Config", fileName = "CraftingHandlerConfig")]
public class CraftingHandlerConfigSO : ScriptableObject
{
    [Header("Crafting")]
    public List<CraftableItemConfigSO> CraftableItems;
}

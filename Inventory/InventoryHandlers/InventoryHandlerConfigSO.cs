using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Inventory Handler Config", fileName = "InventoryHandlerConfig")]
public class InventoryHandlerConfigSO : ScriptableObject
{
    [Header("Inventory")]
    public List<InventoryItemConfigSO> StorableItems;
}

using UnityEngine;

[CreateAssetMenu(menuName = "Consumable Config", fileName = "ConsumableConfig")]
public class ConsumableConfig : ScriptableObject
{
    public InventoryItemConfigSO InventoryItem;

    [Header("Heal")]
    public float HealAmount;
}
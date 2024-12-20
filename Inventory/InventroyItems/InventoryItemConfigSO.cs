using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item Config", fileName = "inventoryItemConfig")]
public class InventoryItemConfigSO : ScriptableObject
{
    [HideInInspector]
    [SerializeField]
    private string serializedItemID;

    private Guid itemID;

    public Guid ItemID
    {
        get
        {
            if (itemID == Guid.Empty && !string.IsNullOrEmpty(serializedItemID))
            {
                itemID = Guid.Parse(serializedItemID);
            }
            return itemID;
        }
    }

    public string Name;
    public Sprite IconSprite;
    public int MaxAmount;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(serializedItemID))
        {
            itemID = Guid.NewGuid();
            serializedItemID = itemID.ToString();
        }
    }
}
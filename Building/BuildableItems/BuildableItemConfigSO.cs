using UnityEngine;

[CreateAssetMenu(menuName = "Building/Buildable Item Config", fileName = "BuildableItemConfig")]
public class BuildableItemConfigSO : ScriptableObject
{
    [Header("Building")]
    public InventoryItemConfigSO InventoryItemType;

    [Header("Audio")]
    public BuildableItemAudioConfigSO AudioConfig;

    [Header("UI")]
    public BuildableItemUIConfigSO UIConfig;

}

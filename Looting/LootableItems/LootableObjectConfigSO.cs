using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PossibleLootItem
{
    public int WeightToDropPerSlot;
    public int MaxAmountToDrop;
    public InventoryItemConfigSO InventoryItem;
}

[CreateAssetMenu(menuName = "Looting/Lootable Object Config", fileName = "LootableObjectConfig")]
public class LootableObjectConfigSO : ScriptableObject
{
    [Header("Information")]
    public string ObjectName;

    [Header("Loot")]
    public List<PossibleLootItem> PossibleLootedItems = new List<PossibleLootItem>();
    public List<int> AmountOfSlotsWeights = new List<int>();
    public float LootTime;

    [Header("UI")]
    public LootableObjectUIConfigSO UIConfig;

    [Header("Audio")]
    public LootableObjectAudioConfigSO AudioConfig;

    // store formated helper texts. Looks ugly in here but there is only one instance of this and its easier to use in inspector. Dont have to make helper texts for each lootable type of object.
    // the original helpers should stay same with unformated strings and only be formated at runtime make it able to change name and keeping the format while having only one instance of this type (not per each object).
    // also makes it so that the formating happens only once per type of object
    // might find better solution but this is the best for now

    private TextHelperConfigSO _formatedCanBeLooted;

    private TimedTextHelperConfigSO _formatedAlreadyLooted;

    private TimedTextHelperConfigSO _formatedNoLootInObject;

    public TextHelperConfigSO GetFormattedCanBeLooted()
    {
        if (_formatedCanBeLooted == null)
        {
            _formatedCanBeLooted = CreateInstance<TextHelperConfigSO>();
            _formatedCanBeLooted.Text = string.Format(UIConfig.CanBeLooted.Text, ObjectName);

            _formatedCanBeLooted.TextColor = UIConfig.CanBeLooted.TextColor;
        }

        return _formatedCanBeLooted;
    }

    public TimedTextHelperConfigSO GetFormattedAlreadyLooted()
    {
        if (_formatedAlreadyLooted == null)
        {
            _formatedAlreadyLooted = CreateInstance<TimedTextHelperConfigSO>();
            _formatedAlreadyLooted.Text = string.Format(UIConfig.AlreadyLooted.Text, ObjectName);

            _formatedAlreadyLooted.TextColor = UIConfig.AlreadyLooted.TextColor;

            _formatedAlreadyLooted.DurationToShow = UIConfig.AlreadyLooted.DurationToShow;
        }

        return _formatedAlreadyLooted;
    }

    public TimedTextHelperConfigSO GetFormattedNoLootInObject()
    {
        if (_formatedNoLootInObject == null)
        {
            _formatedNoLootInObject = CreateInstance<TimedTextHelperConfigSO>();
            _formatedNoLootInObject.Text = string.Format(UIConfig.NoLootInObject.Text, ObjectName);

            _formatedNoLootInObject.TextColor = UIConfig.NoLootInObject.TextColor;

            _formatedNoLootInObject.DurationToShow = UIConfig.NoLootInObject.DurationToShow;
        }

        return _formatedNoLootInObject;
    }
}

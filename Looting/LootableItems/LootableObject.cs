using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LootableObject : MonoBehaviour, ILootable
{
    [SerializeField]
    private LootableObjectConfigSO _lootableObjectConfig;

    [SerializeField]
    private Material _lootedMaterial;

    public bool CanBeLooted // can add more conditions to decide if the object can be looted
    {
        get
        {
            return !_isLooted;
        }
    }

    public float LootingTime => _lootableObjectConfig.LootTime;

    public bool IsLooted => _isLooted;

    public LootableObjectConfigSO LootableObjectConfig => _lootableObjectConfig;

    private bool _isLooted = false;

    private void ChangeMaterialsForImmediateChildren() // this will be replaced in the future with correct item shader handling
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);

        foreach (Renderer renderer in renderers)
        {
            ChangeMaterialsForRenderer(renderer);
        }
    }

    private void ChangeMaterialsForRenderer(Renderer renderer) // this will be replaced in the future with correct item shader handling
    {
        if (renderer != null)
        {
            renderer.material = _lootedMaterial;
        }
        else
        {
            DebugUtils.LogError("Renderer component is null.");
        }
    }

    public TextHelperConfigSO LookedAt()
    {
        if (_isLooted)
            return null;
        else
            return _lootableObjectConfig.GetFormattedCanBeLooted();
    }

    public TimedTextHelperConfigSO FailedTryingLoot()
    {
        return _lootableObjectConfig.GetFormattedAlreadyLooted(); // might add more reason why it cant be looted later
    }

    public TimedTextHelperConfigSO ObjectWasEmpty()
    {
        return _lootableObjectConfig.GetFormattedNoLootInObject();
    }    

    // Generate loot with weighted random on how many slots and what item per slot. (both weighted)
    public List<InventoryItemHolder> GenerateLoot()
    {
        int _amountOfSlots = RandomCalculations.RandomIndexFromWeights(_lootableObjectConfig.AmountOfSlotsWeights);

        DebugUtils.Log("Amount looted: " + _amountOfSlots.ToString());

        List<InventoryItemHolder> _lootedItems = new List<InventoryItemHolder>();

        List<int> _listOfWeights = _lootableObjectConfig.PossibleLootedItems.Select(item => item.WeightToDropPerSlot).ToList();

        for (int i = 0; i < _amountOfSlots; i++)
        {
            int _indexSlotItem = RandomCalculations.RandomIndexFromWeights(_listOfWeights);

            if(_indexSlotItem == -1)
            {
                break;
            }

            int _randomAmount = Random.Range(1, _lootableObjectConfig.PossibleLootedItems[_indexSlotItem].MaxAmountToDrop + 1);
            InventoryItemHolder _newLootedItem = new InventoryItemHolder (_lootableObjectConfig.PossibleLootedItems[_indexSlotItem].InventoryItem, _randomAmount );
            _lootedItems.Add(_newLootedItem);
        }

        foreach(InventoryItemHolder item in _lootedItems)
        {
            DebugUtils.Log("Item: " + item.InventoryItem.Name.ToString() + " Amount: " + item.ItemAmount.ToString());
        }

        _isLooted = true;

        ChangeMaterialsForImmediateChildren();

        return _lootedItems;
    }
}

using UnityEngine;
using System;
using AYellowpaper;

public class DefaultCraftingHandler : MonoBehaviour, ICraftingHandler
{
    [SerializeField]
    private CraftingHandlerConfigSO _craftingHandlerConfigSO;

    [SerializeField]
    private InterfaceReference<IInventoryHandler> _inventoryHandler;

    [SerializeField]
    private InterfaceReference<ICraftingUIHandler> _craftingUIHandler;

    [SerializeField]
    private InputManager _inputManager;

    public CraftingHandlerConfigSO CraftingHandlerConfigSO { get { return _craftingHandlerConfigSO; } }

    public event Action<bool> OnCraftingOpenChanged;

    public event Action<CraftableItemConfigSO, bool> OnCraftingStatusChanged;

    public event Action<CraftableItemConfigSO> OnItemCrafted;

    private CraftingInputHandler _craftingInput;

    private bool _isCrafting = false;

    private bool _isCraftingOpened = false;

    private bool _isCraftingLocked = false;

    private void Start()
    {
        _craftingInput = _inputManager.GetInputHandler<CraftingInputHandler>(EInputCategory.Crafting);
        _craftingInput.OnOpenCraft += HandleOpenCraftingChanged;

        _craftingUIHandler.Value.OnCraftingStatusChanged += HandleCraftingStatusChanged;
        _craftingUIHandler.Value.OnItemCrafted += CraftItem;

        _craftingUIHandler.Value.Initialize(_inventoryHandler.Value, this, _craftingHandlerConfigSO);
    }

    public void HandleLock(bool isLocked)
    {
        _isCraftingLocked = isLocked;
    }

    public void HandleCraftingStatusChanged(CraftableItemConfigSO item, bool isCrafting)
    {
        _isCrafting = isCrafting;

        OnCraftingStatusChanged?.Invoke(item, isCrafting);
    }

    public void HandleOpenCraftingChanged()
    {
        if (_isCraftingLocked)
            return;

        _isCraftingOpened = !_isCraftingOpened;
        OnCraftingOpenChanged?.Invoke(_isCraftingOpened);
    }

    public bool CanCraftItem(CraftableItemConfigSO itemConfig)
    {
        return _inventoryHandler.Value.CheckCraftingMaterialsAvailability(itemConfig.MatsToCraftItem);
    }

    public void CraftItem(CraftableItemConfigSO itemConfig)
    {
        if (CanCraftItem(itemConfig))
        {
            _inventoryHandler.Value.OnCraftedItem(itemConfig);

            OnItemCrafted?.Invoke(itemConfig);
        }
    }
}
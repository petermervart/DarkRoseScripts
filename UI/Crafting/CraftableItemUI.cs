using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using AYellowpaper;

public class CraftableItemUI : MonoBehaviour, ICraftableItemUI, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField]
    private Transform _costParentTransform;

    [SerializeField]
    private GameObject _resourcePrefab;

    [SerializeField]
    private TextMeshProUGUI _nameText;

    [SerializeField]
    private InterfaceReference<IInventoryItemUI> _inventoryItemUI;

    [SerializeField]
    private InterfaceReference<ICraftableImage> _image;

    public event Action<CraftableItemConfigSO, bool> OnCraftingStatusChanged;
    public event Action<CraftableItemConfigSO> OnItemCrafted;

    private CraftableItemConfigSO _craftableItemConfig;

    private HoverScaleUIEffect _scaleUIEffect;

    private IInventoryHandler _inventoryHandler;

    private int _affordableResourcesAmount = 0; // to avoid checking multiple bools... there might be better solution but good for now.
    private bool _canAfford;

    private float _currentFillAmount = 0f;
    private float _cooldownTimer = 0f;
    private bool _isMouseOver = false;
    private bool _isCrafting = false;
    private bool _isCooldown = false;

    private void Awake()
    {
        _scaleUIEffect = GetComponent<HoverScaleUIEffect>();
    }

    public void InitializeCraftingObjectUI()
    {
        _nameText.text = _craftableItemConfig.InventoryItemType.Name;
        _image.Value.Initialize(_craftableItemConfig.InventoryItemType.IconSprite, _craftableItemConfig.UIConfig.NormalColor, _craftableItemConfig.UIConfig.CantAffordColor, _craftableItemConfig.UIConfig.ImageFillMethod);

        _inventoryItemUI.Value.Initialize(_inventoryHandler);

        _inventoryItemUI.Value.ChangeItem(_craftableItemConfig.InventoryItemType);

        _canAfford = false;

        InitializeResourceUIs();
    }

    public void InitializeResourceUIs()
    {
        for (int i = 0; i < _craftableItemConfig.MatsToCraftItem.Count; i++)
        {
            InventoryItemHolder currentItem = _craftableItemConfig.MatsToCraftItem[i];

            GameObject resourceObject = Instantiate(_resourcePrefab, Vector3.zero, Quaternion.identity, _costParentTransform);

            if(resourceObject.TryGetComponent(out ICraftingResourceUI craftingResource))
            {
                craftingResource.InitResource(currentItem.ItemAmount, currentItem.InventoryItem.IconSprite, _craftableItemConfig.UIConfig.NormalColor, _craftableItemConfig.UIConfig.CantAffordColor);

                _inventoryHandler.SubscribeToItemEvent(currentItem.InventoryItem.ItemID, craftingResource.UpdatedAmount);

                craftingResource.OnCanAffordChanged += ResourceAffordChanged;
            }
        }
    }

    public void Initialize(CraftableItemConfigSO craftableItemConfig, IInventoryHandler inventoryHandler)
    {
        _craftableItemConfig = craftableItemConfig;
        _inventoryHandler = inventoryHandler;

        InitializeCraftingObjectUI();
    }

    public void ResourceAffordChanged(bool canAfford)
    {
        _affordableResourcesAmount += canAfford ? 1 : -1;

        bool newCanAfford = _affordableResourcesAmount >= _craftableItemConfig.MatsToCraftItem.Count;

        if(_canAfford != newCanAfford)
        {
            _canAfford = newCanAfford;
            CanAffordChanged();
        }
    }

    public void CanAffordChanged()
    {
        _image.Value.ChangedCanAfford(_canAfford);

        if (_scaleUIEffect != null)
            _scaleUIEffect.ChangeLock(!_canAfford);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isMouseOver = true;
        _currentFillAmount = Mathf.Clamp01(_currentFillAmount); // Ensure the fill is within 0-1 range
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isCrafting) // Call event only if crafting was in progress
        {
            ChangedCraftingState(false);
        }
        _isMouseOver = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isCrafting) // Call event only if crafting was in progress
        {
            ChangedCraftingState(false);
        }
        _isMouseOver = false;
    }

    private void Update()
    {
        if (_isCooldown)
        {
            HandleCooldown();
            return;
        }

        HandleCraftingProgress();
    }

    private void ChangedCraftingState(bool isCrafting)
    {
        _isCrafting = isCrafting;
        OnCraftingStatusChanged?.Invoke(_craftableItemConfig, isCrafting);
    }

    private void HandleCooldown()
    {
        _cooldownTimer += Time.deltaTime;
        _image.Value.UpdateFill(0f);

        if (_cooldownTimer >= _craftableItemConfig.AfterCraftedDelay)
        {
            EndCooldown();
        }
    }

    private void EndCooldown()
    {
        _isCooldown = false;
        _cooldownTimer = 0f;
        _image.Value.UpdateFill(0f);
    }

    private void HandleCraftingProgress()
    {
        if (CanStartCrafting())
        {
            StartCrafting();
            UpdateCraftingProgress();
        }
        else if (!_isMouseOver && _currentFillAmount > 0)
        {
            // If user stops interacting, gradually decrease the fill amount
            DecreaseCraftingProgress();
        }
    }

    private bool CanStartCrafting()
    {
        return !_isCooldown && _isMouseOver && _canAfford;
    }

    private void StartCrafting()
    {
        if (!_isCrafting)
        {
            _isCrafting = true;
            OnCraftingStatusChanged?.Invoke(_craftableItemConfig, _isCrafting);
        }
    }

    private void UpdateCraftingProgress()
    {
        _currentFillAmount += Time.deltaTime / _craftableItemConfig.CraftingTime;
        _currentFillAmount = Mathf.Clamp01(_currentFillAmount);
        _image.Value.UpdateFill(_currentFillAmount);

        if (_currentFillAmount >= 1f)
        {
            CompleteCrafting();
        }
    }

    private void CompleteCrafting()
    {
        ChangedCraftingState(false);
        OnItemCrafted?.Invoke(_craftableItemConfig);

        _isCooldown = true;
        _cooldownTimer = 0f;

        ResetCraftingItem();
    }

    private void DecreaseCraftingProgress()
    {
        // Gradually decrease the fill amount
        _currentFillAmount -= Time.deltaTime / _craftableItemConfig.CraftingTime;
        _currentFillAmount = Mathf.Clamp01(_currentFillAmount); // Ensure the fill is within 0-1 range
        _image.Value.UpdateFill(_currentFillAmount);
    }

    private void ResetCraftingItem()
    {
        _currentFillAmount = 0f;
    }
}
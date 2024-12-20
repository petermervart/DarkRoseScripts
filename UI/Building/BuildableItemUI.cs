using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AYellowpaper;

public class BuildableItemUI : MonoBehaviour, IBuildableItemUI
{
    [SerializeField]
    private TextMeshProUGUI _nameText;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private InterfaceReference<IInventoryItemUI> _inventoryItemUI;

    private HoverScaleUIEffect _scaleUIEffect;

    public event Action<BuildableItemConfigSO> OnChosenBuildable;

    private BuildableItemConfigSO _buildableItemConfig;

    private IInventoryHandler _inventoryHandler;

    private bool _canBuild;

    private void Awake()
    {
        _scaleUIEffect = GetComponent<HoverScaleUIEffect>();
    }

    public void InitializeCraftingObjectUI()
    {
        _nameText.text = _buildableItemConfig.InventoryItemType.Name;
        _image.sprite = _buildableItemConfig.InventoryItemType.IconSprite;

        _inventoryItemUI.Value.Initialize(_inventoryHandler);

        _inventoryItemUI.Value.ChangeItem(_buildableItemConfig.InventoryItemType);

        _inventoryHandler.SubscribeToItemEvent(_buildableItemConfig.InventoryItemType.ItemID, BuildableAmountChanged);

        _canBuild = false;
    }

    public void OnClickedThisBuildable()
    {
        if (!_canBuild)
            return;

        OnChosenBuildable?.Invoke(_buildableItemConfig);
    }

    public void SetColor(Color newColor)
    {
        _image.color = newColor;
        _inventoryItemUI.Value.SetColor(newColor);
    }

    public void BuildableAmountChanged(int amount)
    {
        bool newCanBuild = amount > 0;

        if (newCanBuild == _canBuild)
            return;

        _canBuild = newCanBuild;

        Color newColor = _canBuild ? _buildableItemConfig.UIConfig.NormalColor : _buildableItemConfig.UIConfig.CantBuildColor;

        SetColor(newColor);

        if (_scaleUIEffect != null)
            _scaleUIEffect.ChangeLock(!_canBuild);
    }

    public void Initialize(BuildableItemConfigSO buildableItemConfig, IInventoryHandler inventoryHandler)
    {
        _buildableItemConfig = buildableItemConfig;
        _inventoryHandler = inventoryHandler;

        InitializeCraftingObjectUI();
    }
}
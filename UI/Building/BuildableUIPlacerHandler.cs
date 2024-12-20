using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AYellowpaper;

public class BuildableUIPlacerHandler : SmoothUICanvasGroup, IBuildableUIPlacerHandler
{
    [SerializeField]
    private TextMeshProUGUI _buildableNameText;

    [SerializeField]
    private Image _buildableImage;

    [SerializeField]
    private InterfaceReference<IInventoryItemUI> _buildableInventroyItemUI;

    private IBuildingUIHandler _buildingUIHandler;

    private BuildableItemConfigSO _currentBuildable;

    public void Initialize(IInventoryHandler inventoryHandler, IBuildingUIHandler buildingHandler)
    {
        _buildableInventroyItemUI.Value.Initialize(inventoryHandler);

        _buildableInventroyItemUI.Value.OnNoItemsChanged += NoItemsChanged;

        _buildingUIHandler = buildingHandler;

        _buildingUIHandler.OnBuildableChanged += OnChoseBuildable;

    }

    public void SetColor(Color color)
    {
        _buildableNameText.color = color;
        _buildableImage.color = color;
        _buildableInventroyItemUI.Value.SetColor(color);
    }

    public void NoItemsChanged(bool isThereNoItems)
    {
        SetColor(isThereNoItems ? _currentBuildable.UIConfig.CantBuildColor : _currentBuildable.UIConfig.NormalColor);
    }

    public void OnChoseBuildable(BuildableItemConfigSO newBuildable)
    {
        _currentBuildable = newBuildable;

        Open();

        UpdateBuildable(newBuildable);
    }

    public void UpdateBuildable(BuildableItemConfigSO newBuildable)
    {
        _buildableNameText.text = newBuildable.InventoryItemType.Name;
        _buildableImage.sprite = newBuildable.InventoryItemType.IconSprite;

        _buildableInventroyItemUI.Value.ChangeItem(newBuildable.InventoryItemType);
    }

    public void OnBuildingOpened()
    {
        Close();
    }
}

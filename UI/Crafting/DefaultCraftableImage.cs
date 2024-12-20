using UnityEngine;
using UnityEngine.UI;

public class DefaultCraftableImage : MonoBehaviour, ICraftableImage
{
    [SerializeField]
    private Image _craftableImageForeground;

    [SerializeField]
    private Image _craftableImageBackground;

    private Color _normalColor;
    private Color _cantAffordColor;

    public void Initialize(Sprite logo, Color normalColor, Color cantAffordColor, Image.FillMethod imageFillMethod)
    {
        _craftableImageBackground.sprite = logo;
        _craftableImageForeground.sprite = logo;

        _normalColor = normalColor;
        _cantAffordColor = cantAffordColor;

        _craftableImageForeground.fillMethod = imageFillMethod;
    }

    public void ChangedCanAfford(bool canAfford)
    {
        _craftableImageBackground.color = canAfford ? _normalColor : _cantAffordColor;
    }

    public void UpdateFill(float fillRatio)
    {
        _craftableImageForeground.fillAmount = fillRatio;
    }
}
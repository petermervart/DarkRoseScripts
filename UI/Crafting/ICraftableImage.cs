using UnityEngine;
using UnityEngine.UI;

public interface ICraftableImage
{
    void Initialize(Sprite logo, Color normalColor, Color cantAffordColor, Image.FillMethod imageFillMethod);

    void ChangedCanAfford(bool canAfford);

    void UpdateFill(float fillRatio);
}

using System;
using UnityEngine;

public interface ICraftingResourceUI
{
    public event Action<bool> OnCanAffordChanged;

    public void UpdatedAmount(int amount);

    public void InitResource(int targetAmount, Sprite icon, Color normalColor, Color cantAffordColor);
}

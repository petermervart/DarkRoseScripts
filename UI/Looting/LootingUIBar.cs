using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using AYellowpaper;

public class LootingUIBar : SmoothUICanvasGroup
{
    [SerializeField]
    private InterfaceReference<ISmoothUICanvasGroup> _crosshairGroup;

    [SerializeField]
    private Image _lootingBar;

    public void UpdateLootingProgress(float currentProgress)
    {
        _lootingBar.fillAmount = currentProgress;
    }

    public void HandleOpenChanged(bool isOpen)
    {
        if (isOpen)
            Open();
        else
            Close();
    }

    public override void Open()
    {
        base.Open();
        _crosshairGroup.Value.Close();
        _crosshairGroup.Value.LockElement();
    }

    public override void Close()
    {
        base.Close();
        _crosshairGroup.Value.UnlockElement();
        _crosshairGroup.Value.Open();
    }
}
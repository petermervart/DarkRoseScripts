using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class CraftingResourceUI : MonoBehaviour, ICraftingResourceUI
{
    [SerializeField]
    private TextMeshProUGUI _resourceText;

    [SerializeField]
    private Image _resourceIcon;

    private Color _normalColor;
    private Color _cantAffordColor;

    public event Action<bool> OnCanAffordChanged;

    private int _targetAmount;

    private bool _canAfford;

    private bool _isFirstUpdate = true;

    public void UpdatedAmount(int amount)
    {
        bool newCanAfford = _targetAmount <= amount;

        // Force CanAffordChanged when canAfford (makes later checks in craftable item more efficient) on the first update or if the value changes

        if (_isFirstUpdate) // painful spaghetti if statement but its needed like this in order to be efficient
        {
            _canAfford = newCanAfford;

            if (_canAfford)
                OnCanAffordChanged?.Invoke(_canAfford);

            SetColor(_canAfford);
            SetText(amount);

            _isFirstUpdate = false;
            return;
        }
        
        if (_canAfford != newCanAfford)
        {
            _canAfford = newCanAfford;

            OnCanAffordChanged?.Invoke(_canAfford);

            SetColor(_canAfford);
        }

        SetText(amount);
    }

    public void InitResource(int targetAmount, Sprite icon, Color normalColor, Color cantAffordColor)
    {
        _targetAmount = targetAmount;
        _resourceIcon.sprite = icon;

        _normalColor = normalColor;
        _cantAffordColor = cantAffordColor;
    }
    
    private void SetText(int amount)
    {
        _resourceText.text = amount.ToString() + "/" + _targetAmount.ToString();
    }

    private void SetColor(bool canAfford)
    {
        Color newColor = canAfford ? _normalColor : _cantAffordColor;

        _resourceText.color = newColor;
        _resourceIcon.color = newColor;
    }
}

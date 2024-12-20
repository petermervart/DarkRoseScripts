using System;
using UnityEngine;
using TMPro;

public class GraphicsOptionsUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _graphicsDropdown;

    public event Action<int> OnChangedGraphicsSettings;

    public void InitGraphicsSettings(int graphicsLevelIndex)
    {
        _graphicsDropdown.value = graphicsLevelIndex;
    }

    public void OnGraphicsDropdownChanged()
    {
        OnChangedGraphicsSettings?.Invoke(_graphicsDropdown.value);
    }
}
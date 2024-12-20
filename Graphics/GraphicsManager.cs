using System;
using UnityEngine;

public class GraphicsManager: MonoBehaviour
{
    [SerializeField]
    private GraphicsOptionsUI _graphicsOptionsUI;

    [SerializeField]
    private GraphicsOptionsConfigSO _graphicsOptionsConfig;

    public event Action<int> OnInitGraphicsSettings;

    private void Awake()
    {
        OnInitGraphicsSettings += _graphicsOptionsUI.InitGraphicsSettings;
        _graphicsOptionsUI.OnChangedGraphicsSettings += OnGraphicsChanged;
    }

    public void Start()
    {
        OnInitGraphicsSettings?.Invoke(_graphicsOptionsConfig.CurrentGraphicsSettings);
    }

    public void OnGraphicsChanged(int newSettingsIndex)
    {
        QualitySettings.SetQualityLevel(newSettingsIndex, true);
        _graphicsOptionsConfig.CurrentGraphicsSettings = newSettingsIndex;
    }

}

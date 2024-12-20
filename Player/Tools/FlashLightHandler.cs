using UnityEngine;
using System;

public class FlashLightHandler : MonoBehaviour, IToolHandler
{
    [SerializeField]
    private GameObject _light;

    [SerializeField]
    private InputManager _inputManager;

    public event Action<bool> OnFlashlightOpenChanged;

    private bool _isLightOn = false;

    private bool _isLocked = false;

    private ToolsInputHandler _toolInputHandler;

    private void Start()
    {
        _toolInputHandler = _inputManager.GetInputHandler<ToolsInputHandler>(EInputCategory.Tools);

        _toolInputHandler.OnFlashlight += UsedTool;
    }

    private void UsedTool()
    {
        if (_isLocked)
            return;

        _isLightOn = !_isLightOn;
        _light.SetActive(_isLightOn);
        OnFlashlightOpenChanged?.Invoke(_isLightOn);
    }

    public void LockTool(bool isLocked)
    {
        _isLocked = isLocked;
    }
}

using System;
using UnityEngine;

public class ToolsManager : MonoBehaviour
{
    private bool _isLocked = false;

    private IToolHandler[] _toolHandlers;

    private void Start()
    {
        _toolHandlers = GetComponentsInChildren<IToolHandler>();
    }

    public void SetLock(bool isLocked)
    {
        _isLocked = isLocked;

        for (int i = 0; i < _toolHandlers.Length; i++)
        {
            _toolHandlers[i].LockTool(_isLocked);
        }
    }
}
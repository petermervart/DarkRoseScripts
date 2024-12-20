using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions _playerInput;
    private Dictionary<EInputCategory, IInputHandler> _inputHandlers;

    private void Awake()
    {
        PlayerInputActions playerInput = new PlayerInputActions();

        _playerInput = playerInput;

        _inputHandlers = new Dictionary<EInputCategory, IInputHandler>
        {
            { EInputCategory.Building, new BuildingInputHandler() },
            { EInputCategory.Movement, new MovementInputHandler() },
            { EInputCategory.Interacting, new InteractionsInputHandler() },
            { EInputCategory.Crafting, new CraftingInputHandler() },
            { EInputCategory.Tools, new ToolsInputHandler() },
            { EInputCategory.Attacking, new AttackInputHandler() }
            // Add more handlers as needed
        };

        Initialize();
    }

    public void Initialize()
    {
        foreach (var handler in _inputHandlers.Values)
        {
            handler.Initialize(_playerInput);
        }
    }

    public void Cleanup()
    {
        foreach (var handler in _inputHandlers.Values)
        {
            handler.Cleanup();
        }
    }

    // Method to get a specific input handler
    public T GetInputHandler<T>(EInputCategory category) where T : IInputHandler
    {
        if (_inputHandlers.TryGetValue(category, out var handler))
        {
            return (T)handler;
        }
        throw new ArgumentException($"No input handler found for category {category}");
    }
}
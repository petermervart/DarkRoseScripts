using UnityEngine.InputSystem;

public enum EInputCategory
{
    Movement = 0,
    Combat = 1,
    Interacting = 2,
    Inventory = 3,
    Crafting = 4,
    Building = 5,
    Tools = 6,
    Attacking = 7,
}

public interface IInputHandler
{
    EInputCategory Category { get; }
    void Initialize(PlayerInputActions playerInput);
    void Cleanup();
}

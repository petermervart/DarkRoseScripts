using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackInputHandler : BaseInputHandler
{
    public override EInputCategory Category => EInputCategory.Attacking;

    public event Action<bool> OnAttack;

    private InputAction _attack;

    protected override void BindInputActions()
    {
        _attack = _playerInput.Player.Attack;
        InitActionWithCanceled(_attack, HandleAttack);
    }

    protected override void UnbindInputActions()
    {

    }

    private void HandleAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            OnAttack?.Invoke(true); // Button is pressed
        }
        else if (ctx.canceled)
        {
            OnAttack?.Invoke(false); // Button is released
        }
    }
}
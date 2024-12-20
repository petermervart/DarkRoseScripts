using System;

public interface ICharacterMotor
{
    public event Action<bool> OnRunChanged;
    public event Action<bool> OnIsMovingChanged;
    public event Action OnHitGround;
    public event Action<ESurfaceType> OnFootStep;

    void SetCursorLock(bool locked);
    void SetMovementLock(bool locked);
    void SetLookLock(bool locked);
    void OnStopLookingAndMoving(bool isLooting);
}

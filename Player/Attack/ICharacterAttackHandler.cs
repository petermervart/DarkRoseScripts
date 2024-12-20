using System;

public interface ICharacterAttackHandler
{
    public event Action<bool> OnAttackChanged;

    public event Action<bool> OnHitSomething;

    public void SetLock(bool isLocked);

    public void OnCheckAttack();
}

using System;

public interface ICharacterAnimationHandler : IAnimationHandler
{
    public event Action OnAttack;

    void OnChangedAttack(bool IsAttacking);
    void EndAttackAnimation();

    public void OnChangedRunning(bool IsRunning);

    public void OnChangedMoving(bool IsMoving);
}

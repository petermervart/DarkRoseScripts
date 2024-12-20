using System;

public interface IEnemyAttackHandler
{
    public float MinDistanceToStartAttack { get; }
    public void OnStartAttack();
    public void OnAttack();

    event Action OnStartedAttack;
}

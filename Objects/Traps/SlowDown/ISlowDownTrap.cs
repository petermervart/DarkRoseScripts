using System;

public interface ISlowDownTrap : ITrap
{
    public event Action<bool> OnTrapChangedEnable;

    float SlowDownAffect { get; }

    void EnemyDiedInTrap();
}
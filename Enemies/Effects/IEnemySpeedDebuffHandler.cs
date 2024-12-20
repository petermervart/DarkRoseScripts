using System;

public interface IEnemySpeedDebuffHandler
{
    public event Action OnOwnerDisabled;

    event Action<float> OnSpeedDebuffChanged;

    void ResetSpeedDebuff();

    void AddSlowDownTrap(ISlowDownTrap trap);

    void RemoveSlowDownTrap(ISlowDownTrap trap);

    void OwnerDisabled();
}

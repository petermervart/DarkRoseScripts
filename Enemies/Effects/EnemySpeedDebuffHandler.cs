using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeedDebuffHandler : MonoBehaviour, IEnemySpeedDebuffHandler
{
    private HashSet<ISlowDownTrap> _slowDownTrapsAffecting = new HashSet<ISlowDownTrap>();

    public event Action<float> OnSpeedDebuffChanged;

    public event Action OnOwnerDisabled;

    private float _currentSpeedDebuffAffectRatio;

    public void ResetSpeedDebuff()
    {
        _slowDownTrapsAffecting.Clear();
        _currentSpeedDebuffAffectRatio = 1.0f;
    }

    public void AddSlowDownTrap(ISlowDownTrap trap)
    {
        if (_slowDownTrapsAffecting.Add(trap))  // update modifer only when not in hash map yet - this should not happen but just in case
        {
            UpdateSpeedModifier();
            OnOwnerDisabled += trap.EnemyDiedInTrap;
        }
    }

    public void RemoveSlowDownTrap(ISlowDownTrap trap)
    {
        if(_slowDownTrapsAffecting.Remove(trap)) // update modifer only when exists - this should not happen but just in case
        {
            UpdateSpeedModifier();
            OnOwnerDisabled -= trap.EnemyDiedInTrap;
        }
    }

    public void OwnerDisabled()
    {
        OnOwnerDisabled?.Invoke();

        foreach (ISlowDownTrap trap in _slowDownTrapsAffecting)
        {
            OnOwnerDisabled -= trap.EnemyDiedInTrap;
        }
    }

    private void UpdateSpeedModifier()
    {
        _currentSpeedDebuffAffectRatio = 1.0f;
        foreach (ISlowDownTrap trap in _slowDownTrapsAffecting)
        {
            _currentSpeedDebuffAffectRatio *= trap.SlowDownAffect; // multiply the affect instead of addition, so the affect is not too strong when getting affected by multiple traps
        }

        OnSpeedDebuffChanged?.Invoke(_currentSpeedDebuffAffectRatio);
    }
}
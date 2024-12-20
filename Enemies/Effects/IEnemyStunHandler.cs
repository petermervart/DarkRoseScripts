using System;
using UnityEngine;

public interface IEnemyStunHandler
{
    void ResetStun();
    void OnEnemyStun(float stunTime);

    event Action OnStunned;

    event Action OnStopStunned;
}


using System;
using UnityEngine;

public interface IEnemy : IPoolableObject
{
    public event Action<IEnemy> OnDeath;

    public event Action<GameObject> OnDisable;

    public void InitializeEnemy(Transform playerTransform);

    public void InstaKillEnemy();
}

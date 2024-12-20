using System;
using UnityEngine;

public interface IEnemyMovementHandler
{
    public Transform EnemyTransform { get; }

    event Action<float> OnSpeedRatioChanged;

    event Action<ESurfaceType> OnStep;

    void UpdateMovement();

    void HandleSpeedModifierChange(float modifier);

    void HandleCollider(bool isOn);
}
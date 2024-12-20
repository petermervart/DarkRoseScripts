using System;
using UnityEngine;

public class SlowDownTrap : MonoBehaviour, ISlowDownTrap
{
    [SerializeField]
    private InventoryItemConfigSO _inventoryItemConfig;

    [SerializeField]
    private float _slowDownAffect;

    public InventoryItemConfigSO InventoryItem { get { return _inventoryItemConfig; } }

    public float SlowDownAffect { get { return _slowDownAffect; } }

    public event Action<bool> OnTrapChangedEnable;

    public event Action OnTrapRemoved; // currently not removable but will be in the future

    private float _currentlyAffectedAmount;

    public void Placed()
    {
        gameObject.SetActive(true);
        _currentlyAffectedAmount = 0;
    }

    public void EnemyDiedInTrap()
    {
        RemoveTarget();
    }

    private void HandleTargetEntered(Collider target)
    {
        if (target.gameObject.TryGetComponent<IEnemySpeedDebuffHandler>(out var enemySpeedDebuffHandler))
        {
            enemySpeedDebuffHandler.AddSlowDownTrap(this);

            AddTarget();
        }
    }

    private void HandleTargetExited(Collider target)
    {
        // if 0 enemies are in the trap stop playing the sound

        if (target.gameObject.TryGetComponent<IEnemySpeedDebuffHandler>(out var enemySpeedDebuffHandler))
        {
            enemySpeedDebuffHandler.RemoveSlowDownTrap(this);

            RemoveTarget();
        }
    }

    private void AddTarget()
    {
        if (_currentlyAffectedAmount == 0)
            OnTrapChangedEnable?.Invoke(true);

        _currentlyAffectedAmount += 1;
    }

    private void RemoveTarget()
    {
        _currentlyAffectedAmount -= 1;

        if (_currentlyAffectedAmount == 0)
            OnTrapChangedEnable?.Invoke(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleTargetEntered(other);
    }

    private void OnTriggerExit(Collider other)
    {
        HandleTargetExited(other);
    }
}

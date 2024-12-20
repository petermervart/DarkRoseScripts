using System;
using System.Collections;
using UnityEngine;

public class StunTrap : MonoBehaviour, IStunTrap
{
    [SerializeField]
    private InventoryItemConfigSO _inventoryItemConfig;

    [SerializeField] 
    private float _trapActivationDelay = 0.3f;

    [SerializeField]
    private float _timeToDisappearInSeconds = 0.5f;

    [SerializeField]
    private float _stunDurationInSeconds = 0.5f;

    [SerializeField]
    private Animator _trapAnimator;

    public InventoryItemConfigSO InventoryItem { get { return _inventoryItemConfig; } }

    public event Action OnTrapActivated;

    public event Action OnTrapHit;

    public event Action<float> OnStunned;

    public event Action OnTrapRemoved;

    private IEnemyStunHandler _hitTarget;

    public void Placed()
    {
        gameObject.SetActive(true);
    }

    // trap can affect only one enemy
    private void HandleTargetEntered(Collider target)
    {
        if (_hitTarget != null)
        {
            if (target.gameObject.TryGetComponent(out _hitTarget))
            {
                _trapAnimator.SetTrigger("activate");
                StartCoroutine(TrapActivateCoroutine());
            }
        }
    }

    private void TrapActivated()
    {
        OnTrapActivated?.Invoke();
    }

    private void TrapHit()
    {
        OnStunned += _hitTarget.OnEnemyStun;

        OnStunned.Invoke(_stunDurationInSeconds); // might change this to just calling the script but there might be more listeners in the future.

        OnStunned -= _hitTarget.OnEnemyStun;

        OnTrapHit?.Invoke();
    }

    private void TrapRemoved()
    {
        _hitTarget = null;
        OnTrapRemoved?.Invoke();
        gameObject.SetActive(false);
    }

    private IEnumerator TrapActivateCoroutine()
    {
        TrapActivated();

        yield return new WaitForSeconds(_trapActivationDelay);

        TrapHit();

        yield return new WaitForSeconds(_timeToDisappearInSeconds);

        TrapRemoved();
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleTargetEntered(other);
    }
}
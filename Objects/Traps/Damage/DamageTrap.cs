using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageTrap : MonoBehaviour, IDamageTrap
{
    [SerializeField]
    private InventoryItemConfigSO _inventoryItemConfig;

    [SerializeField]
    private float _trapActivationDelay = 0.3f;

    [SerializeField]
    private float _timeToDisappearInSeconds = 0.5f;

    [SerializeField]
    private Vector2 _damageRange;

    [SerializeField]
    private Animator _trapAnimator; // should make this separate component but good for now

    public InventoryItemConfigSO InventoryItem { get { return _inventoryItemConfig; } }

    public event Action OnTrapActivated;

    public event Action OnTrapHit;

    public event Action OnTrapRemoved;

    private IHealth _hitTarget;

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
        _hitTarget.TakeDamage(Random.Range(_damageRange.x, _damageRange.y));

        OnTrapHit?.Invoke();
    }

    private IEnumerator TrapActivateCoroutine()
    {
        TrapActivated();

        yield return new WaitForSeconds(_trapActivationDelay);

        TrapHit();

        yield return new WaitForSeconds(_timeToDisappearInSeconds);

        TrapRemoved();
    }

    private void TrapRemoved()
    {
        _hitTarget = null;
        OnTrapRemoved?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleTargetEntered(other);
    }
}
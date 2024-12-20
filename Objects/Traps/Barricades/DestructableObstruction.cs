using System;
using UnityEngine;

public class DestructableObstruction : MonoBehaviour, IDestructableObstruction
{
    [SerializeField]
    private InventoryItemConfigSO _inventoryItemConfig;

    [SerializeField]
    private float _maxHealth;

    public InventoryItemConfigSO InventoryItem { get { return _inventoryItemConfig; } }

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;
    public bool IsHealthDepleted => _currentHealth <= 0;

    public event Action OnGotDamaged; // if really dealt damage (for example if cant destroy it, this will not play)

    public event Action OnHealthDepleted;

    public event Action OnTrapRemoved;

    public event Action OnGotHit; // if it got hit

    private float _currentHealth;

    public async void Placed()
    {
        gameObject.SetActive(true);
        ResetHealth();
        await NavMeshAreaBaker.Instance.BuildNavMesh(true);
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }

    public bool TakeDamage(float damage)  // returns bool (if the target died or not)
    {
        OnGotHit?.Invoke();

        if (IsHealthDepleted) return false;

        _currentHealth -= damage;

        OnGotDamaged?.Invoke();

        DebugUtils.Log($"{gameObject.name}: Damaged {damage}.");

        if (_currentHealth <= 0)
        {
            HealthDepleted();
            return true;
        }

        return false;
    }

    public void HealthDepleted()
    {
        _currentHealth = 0;
        OnHealthDepleted?.Invoke();

        TrapRemoved();
    }

    public void InstaDepleteHealth()
    {
        HealthDepleted();
    }

    private async void TrapRemoved()
    {
        await NavMeshAreaBaker.Instance.BuildNavMesh(true);

        OnTrapRemoved?.Invoke();

        gameObject.SetActive(false);
    }
}
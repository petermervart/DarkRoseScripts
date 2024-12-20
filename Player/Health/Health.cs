using System.Collections;
using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField]
    private float _maxHealth;

    [SerializeField]
    private float _timeStunnedAfterAttack;

    public float TimeStunnedAfterAttack { get => _timeStunnedAfterAttack; }

    private float _currentHealth;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;
    public bool IsHealthDepleted => _currentHealth <= 0;

    public event Action OnGotDamaged;

    public event Action OnGotHit; // replace this with surface particle and sound system in the future

    public event Action OnHealthDepleted;

    public event Action OnHealed;

    public event Action<float> OnHealthChanged;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void InstaDepleteHealth()
    {
        HealthDepleted();
    }

    public bool TakeDamage(float damage)  // returns bool (if the target died or not)
    {
        OnGotHit?.Invoke(); // even if dead hit impact sound should play

        if (IsHealthDepleted) return false;

        _currentHealth -= damage;

        OnGotDamaged?.Invoke();

        OnHealthChanged?.Invoke(_currentHealth / _maxHealth);

        DebugUtils.Log($"{gameObject.name}: Damaged {damage}.");

        if (_currentHealth <= 0)
        {
            HealthDepleted();
            return true;
        }

        return false;
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }

    public void HealthDepleted()
    {
        _currentHealth = 0;
        OnHealthDepleted?.Invoke();
    }

    public bool CanHeal()
    {
        if (IsHealthDepleted || _currentHealth == _maxHealth)
            return false;

        return true;
    }

    public bool Heal(float amount) // returns bool (if the target was really healed or not)
    {
        if (IsHealthDepleted || _currentHealth == _maxHealth) return false;

        _currentHealth += amount;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        DebugUtils.Log($"{gameObject.name}: Healed {amount}.");

        OnHealed?.Invoke();

        OnHealthChanged.Invoke(_currentHealth);

        return true;
    }
}
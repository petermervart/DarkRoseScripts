using System;

public interface IDamageable
{
    bool TakeDamage(float damage);
    void ResetHealth();
    void InstaDepleteHealth();
    void HealthDepleted();
    float CurrentHealth { get; }
    float MaxHealth { get; }

    bool IsHealthDepleted { get; }

    event Action OnGotDamaged;

    event Action OnGotHit;

    event Action OnHealthDepleted;
}

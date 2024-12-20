using System;

public interface IHealth : IDamageable
{
    bool Heal(float amount);
    bool CanHeal();
    event Action OnHealed;
    event Action<float> OnHealthChanged; // health ratio right now... might change later
}

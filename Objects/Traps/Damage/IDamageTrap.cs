using System;

public interface IDamageTrap : ITrap
{
    public event Action OnTrapActivated;

    public event Action OnTrapHit;
}

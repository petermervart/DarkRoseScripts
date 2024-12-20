using System;

public interface IStunTrap : ITrap
{
    public event Action OnTrapActivated;
}
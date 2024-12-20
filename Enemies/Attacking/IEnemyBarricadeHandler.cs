using System;

public interface IEnemyBarricadeHandler
{
    void OnUpdateBarricade();

    public event Action OnDetectedObstruction;

    public event Action OnObstructionDestroyed;
}

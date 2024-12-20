
public interface IEnemyHealth : IHealth
{
    public float TimeStunnedAfterAttack { get; }
    public float TimeToDisappearAfterDeath { get; }
}
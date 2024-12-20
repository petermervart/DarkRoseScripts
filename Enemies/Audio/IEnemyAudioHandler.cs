
public interface IEnemyAudioHandler
{
    void OnGotHurt();

    void OnDeath();

    void OnAttack();

    void OnHitImpact();

    void OnStep(ESurfaceType surfaceType);
}

using System;

public interface IEnemySpawner
{
    public void Initialize(EnemyManager enemyManager);

    public void OnEnemyDied(IEnemy enemy);

    public void StartSpawnEnemies();

    public void RemoveAllEnemies();

    event Action<IEnemySpawner> OnOutOfEnemies;
}

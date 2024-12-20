using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    [SerializeField]
    private float _maxDistanceFromRandomStartPoint = 5.0f;  //Max distance from random point next to NavMesh (The point does not have to be on valid NavMesh, so closest point is chosen)

    [SerializeField]
    private NavMeshSurface _surface;

    [SerializeField]
    private SpawnerConfigSO _spawnerConfig;

    public event Action<IEnemySpawner> OnOutOfEnemies;

    private PoolManager _enemyPool;

    private EnemyManager _enemyManager;

    private List<IEnemy> _activeEnemies = new List<IEnemy>();

    private int _allSpawnedEnemiesAmount;

    private bool _isSpawnAmountDepleted = false;

    public void Initialize(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
        _enemyPool = new PoolManager(_spawnerConfig.EnemyPrefab, null, _spawnerConfig.EnemyPoolDefaultSize, _spawnerConfig.EnemyPoolMaxSize);
    }

    public void OnEnemyDied(IEnemy enemy) // called when enemy dies
    {
        _activeEnemies.Remove(enemy); // remove enemy from active (alive) enemies only

        if (!(_allSpawnedEnemiesAmount < _spawnerConfig.MaxEnemiesAmountPerLevel))
        {
            _isSpawnAmountDepleted = true;
            OnOutOfEnemies?.Invoke(this);  // if player kills all the enemies possible he wins
        }
    }

    public void OnEnemyDestroyed(GameObject enemyObject)
    {
        _enemyPool.Return(enemyObject);

        if (_isSpawnAmountDepleted)
            return;

        SpawnNavMeshAgentOnBorder();
    }

    public void RemoveAllEnemies()
    {
        foreach(IEnemy enemy in _activeEnemies)
        {
            enemy.InstaKillEnemy();
        }
    }

    public void StartSpawnEnemies()
    {
        for (int index = 0; index < _spawnerConfig.MaxEnemiesAmountAtOneTime; index++)
        {
            SpawnNavMeshAgentOnBorder();
        }
    }

    private void SpawnNavMeshAgentOnBorder()
    {
        Vector3 randomPoint = RandomCalculations.GetRandomPointOnNavMeshBorder(_enemyManager.GetPlayerTransform().position, _enemyManager.GetNavMeshMaxDistance(), _spawnerConfig.MinDistanceFromPlayer, _surface, GetNavMeshMask(), _maxDistanceFromRandomStartPoint);

        GameObject newEnemy = _enemyPool.Get();
        newEnemy.transform.SetPositionAndRotation(randomPoint, Quaternion.identity);

        if (newEnemy.TryGetComponent<IEnemy>(out var enemyComponent)) // could remove this and use custom pool manager just for enemies but this way we can use generic pool manager.
        {
            enemyComponent.InitializeEnemy(_enemyManager.GetPlayerTransform());
            enemyComponent.OnDeath += OnEnemyDied;
            enemyComponent.OnDisable += OnEnemyDestroyed;
            _activeEnemies.Add(enemyComponent);
        }
        else
        {
            DebugUtils.LogWarning("Prefab on Enemy Spawner " + gameObject.name + " is not an Enemy!");
        }

        _allSpawnedEnemiesAmount++;

    }

    private int GetNavMeshMask()  // Build NavMeshMask to make sure enemy is not spawned on not allowed area
    {
        int mask = 0;
        mask += 1 << NavMesh.GetAreaFromName("Walkable");
        mask += 0 << NavMesh.GetAreaFromName("Not walkable");
        mask += 0 << NavMesh.GetAreaFromName("Jump");
        mask += 0 << NavMesh.GetAreaFromName("Destructable");

        return mask;
    }
}
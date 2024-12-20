using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private NavMeshAreaBaker _navMeshBaker;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private LevelManager _levelManager;

    public event Action OnKilledAllEnemies;

    private Vector3 _navMeshMaxDistance;

    private int _depletedSpawnersCount = 0;

    private IEnemySpawner[] _enemySpawners;

    private void Awake()
    {
        _enemySpawners = GetComponentsInChildren<IEnemySpawner>();
    }

    private void Start()
    {
        OnKilledAllEnemies += _levelManager.LoadSurvived;
        CalculateNavMeshDistances();
        InitializeSpawners();
    }

    public void OnDepletedSpawner(IEnemySpawner spawner)
    {
        _depletedSpawnersCount++;

        spawner.OnOutOfEnemies -= OnDepletedSpawner;

        if (_depletedSpawnersCount == _enemySpawners.Length)
            OnKilledAllEnemies?.Invoke();
    }

    public void RemoveAllEnemies()
    {
        for (int i = 0; i < _enemySpawners.Length; i++)
        {
            _enemySpawners[i].RemoveAllEnemies();
        }
    }

    private void InitializeSpawners()
    {
        for (int i = 0; i < _enemySpawners.Length; i++)
        {
            _enemySpawners[i].Initialize(this);
            _enemySpawners[i].OnOutOfEnemies += OnDepletedSpawner;
        }
    }

    private void CalculateNavMeshDistances()
    {
        _navMeshMaxDistance = _navMeshBaker.GetTheNavMeshSize() / 2.0f;
    }

    public void StartSpawnEnemies()
    {
        for (int i = 0; i < _enemySpawners.Length; i++)
        {
            _enemySpawners[i].StartSpawnEnemies();
        }
    }

    public Vector3 GetNavMeshMaxDistance()
    {
        return _navMeshMaxDistance;
    }

    public Transform GetPlayerTransform()
    {
        return _player;
    }
}

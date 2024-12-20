using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawner Config", fileName = "SpawnerConfig")]

// Config for enemy spawning
public class SpawnerConfigSO : ScriptableObject
{
    [Header("Enemies Settings")]
    public int MaxEnemiesAmountAtOneTime;
    public int EnemyPoolDefaultSize;
    public int EnemyPoolMaxSize;
    public int MaxEnemiesAmountPerLevel;
    public GameObject EnemyPrefab;

    [Header("Spawning Settings")]
    public Vector3 MinDistanceFromPlayer;
}
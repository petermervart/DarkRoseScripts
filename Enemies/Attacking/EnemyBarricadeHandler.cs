using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBarricadeHandler : MonoBehaviour, IEnemyBarricadeHandler
{
    [Header("Components")]

    [SerializeField]
    private NavMeshAgent _agent;

    [Header("Settings")]

    [SerializeField]
    private float _obstructionCheckRate;

    [SerializeField]
    private float _minDistanceToStartDestroying;

    [SerializeField]
    private LayerMask _obstructionsLayerMask;

    [SerializeField]
    private float _obstructionAttackDelay;

    [SerializeField]
    private float _obstructionTimeBetweenAttacks;

    [SerializeField]
    private float _obstructionMinDamage;

    [SerializeField]
    private float _obstructionMaxDamage;

    public event Action OnDetectedObstruction;

    public event Action OnObstructionDestroyed;

    private IDestructableObstruction _currentObstruction;

    private float _timeSinceLastCheck;

    private bool _canAttack = true;

    private float _timeSinceLastAttack = 0f;

    private bool _isDestroyingObstruction;

    public void OnUpdateBarricade()
    {
        if (_isDestroyingObstruction)
            DamageDestructibleObstruction();
        else
            CheckForDestructibleObstructions();
    }

    private void CheckForDestructibleObstructions()
    {
        _timeSinceLastCheck += Time.deltaTime;

        if (_timeSinceLastCheck > _obstructionCheckRate)
        {
            _timeSinceLastCheck = 0f;

            Vector3[] corners = new Vector3[2];
            int length = _agent.path.GetCornersNonAlloc(corners);

            // Check if the barricade is in front of the enemy, if it is start destroying it

            if (length > 1 && Physics.Raycast(corners[0], (corners[1] - corners[0]).normalized, out RaycastHit hit, _minDistanceToStartDestroying, _obstructionsLayerMask))
            {
                if (hit.collider.TryGetComponent(out IDestructableObstruction destructibleObstruction))
                {
                    _isDestroyingObstruction = true;
                    _currentObstruction = destructibleObstruction;
                    destructibleObstruction.OnHealthDepleted += OnDestructableDestroyed;
                    OnDetectedObstruction?.Invoke();
                }
            }
        }
    }

    private void DamageDestructibleObstruction()
    {
        _timeSinceLastAttack += Time.deltaTime;

        if (_canAttack && _timeSinceLastAttack >= _obstructionAttackDelay)
        {
            PerformObstructionAttack();
        }
        else if (!_canAttack && _timeSinceLastAttack >= _obstructionTimeBetweenAttacks)
        {
            ResetObstructionAttack();
        }
    }

    private void PerformObstructionAttack()
    {
        _currentObstruction.TakeDamage(Random.Range(_obstructionMinDamage, _obstructionMaxDamage));
        _canAttack = false;
        _timeSinceLastAttack = 0f;
    }

    private void ResetObstructionAttack()
    {
        _canAttack = true;
        _timeSinceLastAttack = 0f;
    }

    private void OnDestructableDestroyed()
    {
        _isDestroyingObstruction = false;
        _currentObstruction = null;
        OnObstructionDestroyed?.Invoke();
    }
}
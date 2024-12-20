using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttackHandler : MonoBehaviour, IEnemyAttackHandler
{
    [SerializeField]
    private float _minDistanceToStartAttack;

    [SerializeField]
    private float _attackMaxAngle;

    [SerializeField]
    private float _minDistanceToHitAttack;

    [SerializeField]
    private LayerMask _attackLayerMask;

    [SerializeField]
    private Vector2 _damageRange;

    public float MinDistanceToStartAttack { get => _minDistanceToStartAttack; }

    public event Action OnStartedAttack;

    private float radians;

    private void Start()
    {
        radians = _attackMaxAngle * Mathf.Deg2Rad;
    }

    public void OnStartAttack()
    {
        OnStartedAttack?.Invoke();
    }

    public void OnAttack()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _minDistanceToHitAttack, transform.forward, 0f, _attackLayerMask);

        foreach (RaycastHit hit in hits)
        {
            // Spherecast with angle check
            Vector3 directionToTarget = (hit.transform.position - transform.position).normalized;
            float dotProduct = Vector3.Dot(transform.forward, directionToTarget);

            if (dotProduct >= Mathf.Cos(radians))
            {
                if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.TakeDamage(Random.Range(_damageRange.x, _damageRange.y));
                }
            }
        }
    }


}
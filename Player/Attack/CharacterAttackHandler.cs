using UnityEngine;
using Random = UnityEngine.Random;
using System;

public class CharacterAttackHandler : MonoBehaviour, ICharacterAttackHandler
{
    [Header("Input")]

    [SerializeField]
    private InputManager _inputManager;

    [Header("Attacking")]

    [SerializeField]
    private float _minDistanceToHitAttack = 2f;

    [SerializeField]
    private LayerMask _attackLayerMask = ~0;

    [SerializeField]
    private float _minDamage = 20;

    [SerializeField]
    private float _maxDamage = 40;

    [SerializeField]
    private float _attackMaxAngle = 45f;

    [SerializeField]
    private Transform _camera;

    [Header("Particles")] // this will be replaced completly with new surface particle system in the future... just for now while testing blood shaders

    [SerializeField]
    private GameObject _bloodParticle;

    [SerializeField]
    private Vector2 _bloodParticleRandomXOffset;

    [SerializeField]
    private Vector2 _bloodParticleRandomYOffset;

    [SerializeField]
    private Vector2 _bloodParticleRandomZOffset;

    public event Action<bool> OnAttackChanged;

    public event Action<bool> OnHitSomething;

    private AttackInputHandler _attackInputHandler;

    private bool _isAttackLocked = false;

    private bool _isAttacking = false;

    private void Start()
    {
        _attackInputHandler = _inputManager.GetInputHandler<AttackInputHandler>(EInputCategory.Attacking);

        _attackInputHandler.OnAttack += OnAttack;
    }

    public void SetLock(bool isLocked)
    {
        _isAttackLocked = isLocked;
    }

    protected void OnAttack(bool isPressed)
    {
        if (_isAttackLocked)
            return;

        if (isPressed && !_isAttacking)
        {
            OnStartAttack();
        }
        else if (!isPressed && _isAttacking)
        {
            OnEndAttack();
        }
    }

    protected void CheckForAttackHit()
    {
        if (_isAttackLocked)
            return;

        float radians = _attackMaxAngle * Mathf.Deg2Rad;

        Vector3 forwardDirection = transform.forward;

        RaycastHit[] hits = Physics.RaycastAll(_camera.position, forwardDirection, _minDistanceToHitAttack, _attackLayerMask);

        bool hitSomething = false;

        foreach (RaycastHit hit in hits)
        {
            Vector3 directionToTarget = (hit.collider.bounds.center - _camera.position).normalized;

            float dotProduct = Vector3.Dot(forwardDirection, directionToTarget);

            if (dotProduct >= Mathf.Cos(radians))
            {
                if (!hitSomething)
                    hitSomething = true;

                if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.TakeDamage(Random.Range(_minDamage, _maxDamage));

                    // spawning of VFX will not be here in the future

                    GameObject bloodParticle = Instantiate(_bloodParticle);
                    bloodParticle.transform.position = hit.point;
                    bloodParticle.transform.forward = hit.normal;

                    var cameraHandler = bloodParticle.GetComponent<CameraDepthBufferManager>();
                    if (cameraHandler != null)
                        cameraHandler.OnRender();

                }
            }
        }

        OnHitSomething?.Invoke(hitSomething);
    }

    public void OnCheckAttack() // this is called from animator at certain frame
    {
        CheckForAttackHit();
    }

    protected void OnStartAttack()
    {
        OnAttackChanged?.Invoke(true);
        _isAttacking = true;
    }

    protected void OnEndAttack()
    {
        OnAttackChanged?.Invoke(false);
        _isAttacking = false;
    }
}

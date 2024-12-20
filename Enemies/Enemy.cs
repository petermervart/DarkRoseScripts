using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using AYellowpaper;

public class Enemy : MonoBehaviour, IEnemy
{
    [Header("Components")]
    [SerializeField]
    private InterfaceReference<IEnemyAudioHandler> _audioHandler;

    [SerializeField]
    private InterfaceReference<IEnemyHealth> _health;

    [SerializeField]
    private InterfaceReference<IEnemyAnimationHandler> _animationHandler;

    [SerializeField]
    private InterfaceReference<INavigationHandler> _navigationHandler;

    [SerializeField]
    private InterfaceReference<IEnemyStunHandler> _stunHandler;

    [SerializeField]
    private InterfaceReference<IEnemySpeedDebuffHandler> _speedDebuffHandler;

    [SerializeField]
    private InterfaceReference<IEnemyMovementHandler> _movementHandler;

    [SerializeField]
    private InterfaceReference<IEnemyBarricadeHandler> _barricadeHandler;

    [SerializeField]
    private InterfaceReference<IEnemyAttackHandler> _attackHandler;

    private IEnemyAudioHandler AudioHandler => _audioHandler.Value;

    private IEnemyHealth HealthHandler => _health.Value; // could remove these properties but code is cleaner with them.

    private IEnemyAnimationHandler AnimationHandler => _animationHandler.Value;

    private INavigationHandler NavigationHandler => _navigationHandler.Value;

    private IEnemyStunHandler StunHandler => _stunHandler.Value;

    private IEnemySpeedDebuffHandler SpeedDebuffHandler => _speedDebuffHandler.Value;

    private IEnemyMovementHandler MovementHandler => _movementHandler.Value;

    private IEnemyBarricadeHandler BarricadeHandler => _barricadeHandler.Value;

    private IEnemyAttackHandler AttackHandler => _attackHandler.Value;

    [Header("State Machine")]
    public FollowState FollowState { get; private set; }
    public AttackState AttackState { get; private set; }
    public StunnedState StunnedState { get; private set; }
    public DestroyingBarricadeState DestroyingBarricadeState { get; private set; }
    public DamageStunnedState DamageStunnedState { get; private set; }
    public DeadState DeadState { get; private set; }

    [SerializeField] private StateManager _stateManager;

    public UnityEvent OnGotHurt = new UnityEvent();
    public UnityEvent OnDeathEvent = new UnityEvent();
    public UnityEvent OnGotHitImpact = new UnityEvent();

    public event Action<IEnemy> OnDeath;
    public event Action<GameObject> OnDisable;

    private void Awake()
    {
        InitializeStates();
    }

    private void Start()
    {
        InitializeHealth();

        InitializeStunHandler();

        InitializeSpeedDebuffHandler();

        InitializeNavigationHandler();

        InitializeMovementHandler();

        InitializeBarricadeHandler();

        InitializeAttackHandler();
    }

    private void OnDestroy()
    {
        HealthHandler.OnGotDamaged -= HandleGotDamaged;
        HealthHandler.OnHealthDepleted -= HandleDeath;
        StunHandler.OnStunned -= HandleStun;
    }

    #region HandlerInits

    private void InitializeAudio()
    {
        if (AudioHandler == null) return;
    }

    private void InitializeHealth()
    {
        if (HealthHandler == null) return;

        HealthHandler.OnGotDamaged += HandleGotDamaged;
        HealthHandler.OnGotDamaged += AudioHandler.OnGotHurt;
        HealthHandler.OnGotHit += AudioHandler.OnHitImpact;
        HealthHandler.OnHealthDepleted += HandleDeath;
        HealthHandler.OnHealthDepleted += SpeedDebuffHandler.OwnerDisabled;
        HealthHandler.OnHealthDepleted += AudioHandler.OnDeath;
    }

    private void InitializeSpeedDebuffHandler()
    {
        if (SpeedDebuffHandler == null) return;

        SpeedDebuffHandler.OnSpeedDebuffChanged += MovementHandler.HandleSpeedModifierChange;
    }

    private void InitializeStunHandler()
    {
        if (StunHandler == null) return;

        StunHandler.OnStunned += HandleStun;
    }

    private void InitializeNavigationHandler()
    {
        if (NavigationHandler == null) return;

    }

    private void InitializeMovementHandler()
    {
        if (MovementHandler == null) return;

        MovementHandler.OnSpeedRatioChanged += AnimationHandler.ChangeRunAnimationSpeed;
        MovementHandler.OnStep += AudioHandler.OnStep;
    }

    private void InitializeBarricadeHandler()
    {
        if (BarricadeHandler == null) return;

        BarricadeHandler.OnDetectedObstruction += HandleBarricade;
    }

    public void InitializeAttackHandler()
    {
        if(AttackHandler == null) return;

        AttackHandler.OnStartedAttack += AudioHandler.OnAttack;
    }
    #endregion

    private void InitializeStates()
    {
        FollowState = new FollowState(this, NavigationHandler, BarricadeHandler, MovementHandler, AttackHandler);
        AttackState = new AttackState(this, AnimationHandler, NavigationHandler, MovementHandler, AttackHandler);
        StunnedState = new StunnedState(this, AnimationHandler, NavigationHandler, StunHandler, MovementHandler, AttackHandler);
        DestroyingBarricadeState = new DestroyingBarricadeState(this, AnimationHandler, BarricadeHandler, NavigationHandler, MovementHandler);
        DamageStunnedState = new DamageStunnedState(this, HealthHandler, AnimationHandler, NavigationHandler, AttackHandler, MovementHandler);
        DeadState = new DeadState(this, HealthHandler, AnimationHandler, NavigationHandler);
    }

    public void InitializeEnemy(Transform player)
    {
        NavigationHandler.SetTarget(player);
        _stateManager.ChangeState(FollowState);
        HealthHandler.ResetHealth();
        SpeedDebuffHandler.ResetSpeedDebuff();
        StunHandler.ResetStun();
    }

    public void OnSpawn()
    {
        // Not doing anything on spawn yet 
    }

    public void InstaKillEnemy()
    {
        HealthHandler.InstaDepleteHealth();
    }

    private void HandleGotDamaged()
    {
        if (_stateManager.CheckCurrentState(StunnedState)) return; // States from which damageStun should not be applied

        _stateManager.ChangeState(DamageStunnedState);
    }

    public void HandleStun()
    {
        _stateManager.ChangeState(StunnedState);
    }

    public void HandleBarricade()
    {
        _stateManager.ChangeState(DestroyingBarricadeState);
    }

    private void HandleDeath() // Enemy Died
    {
        _stateManager.ChangeState(DeadState);
        OnDeath.Invoke(this);
    }

    public void OnDisableObject() // Enemy Disabled
    {
        OnDisable?.Invoke(gameObject);
    }

    public void OnDespawn()
    {
        _stateManager.ChangeState(null);
    }
}

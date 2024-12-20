using System;
using UnityEngine;
using AYellowpaper;

public class Character : MonoBehaviour, ICharacter
{
    [Header("Components")]
    [SerializeField]
    private InterfaceReference<ICharacterMotor> _characterMotor;

    [SerializeField]
    private InterfaceReference<ICharacterAttackHandler> _attackHandler;

    [SerializeField]
    private InterfaceReference<IHealth> _healthHandler;

    [SerializeField]
    private InterfaceReference<ICharacterAnimationHandler> _animationHandler;

    [SerializeField]
    private InterfaceReference<ICharacterCameraEffects> _cameraEffects;

    [SerializeField]
    private InterfaceReference<ICharacterAudioHandler> _audioHandler;

    [SerializeField]
    private ToolsManager _toolsManager;

    [SerializeField]
    private LootingManager _lootingManager;

    [SerializeField]
    private CraftingManager _craftingManager;

    [SerializeField]
    private BuildingManager _buildingManager;

    [SerializeField]
    private InteractionsManager _interactionsManager;

    [Header("State Machine")]
    [SerializeField] private StateManager _stateManager;

    public event Action OnDeath;

    public CharacterDefaultState DefaultState { get; private set; }
    public CharacterLootingState LootingState { get; private set; }
    public CharacterCraftingState CraftingState { get; private set; }
    public CharacterBuildingState BuildingState { get; private set; }
    public CharacterPlacingBuildableState PlacingBuildingState { get; private set; }

    private void Awake()
    {
        InitializeStates();
    }

    private void Start()
    {
        InitializeMotorHandler();

        InitializeHealthHandler();

        InitializeAttackHandler();

        InitializeAnimationHandler();

        _stateManager.ChangeState(DefaultState);
    }

    #region HandlerInits

    private void InitializeMotorHandler()
    {
        if (_characterMotor.Value == null) return;

        _characterMotor.Value.OnHitGround += _audioHandler.Value.OnHitGround;
        _characterMotor.Value.OnFootStep += _audioHandler.Value.OnFootSteps;

        _characterMotor.Value.OnRunChanged += _animationHandler.Value.OnChangedRunning;
        _characterMotor.Value.OnRunChanged += _cameraEffects.Value.ChangedRun;

        _characterMotor.Value.OnIsMovingChanged += _animationHandler.Value.OnChangedMoving;
    }

    private void InitializeHealthHandler()
    {
        if (_healthHandler.Value == null) return;

        _healthHandler.Value.OnGotDamaged += _audioHandler.Value.OnGotHurt;

        _healthHandler.Value.OnHealthDepleted += HandleDeath;
    }

    private void InitializeAttackHandler()
    {
        if (_attackHandler.Value == null) return;

        _attackHandler.Value.OnHitSomething += _audioHandler.Value.OnHit;

        _attackHandler.Value.OnAttackChanged += _animationHandler.Value.OnChangedAttack;
    }

    private void InitializeAnimationHandler()
    {
        if (_animationHandler.Value == null) return;

        _animationHandler.Value.OnAttack += _attackHandler.Value.OnCheckAttack;

        _attackHandler.Value.OnAttackChanged += _animationHandler.Value.OnChangedAttack;
    }

    public void HandleDeath()
    {
        OnDeath?.Invoke();
    }

    #endregion

    private void InitializeStates()
    {
        DefaultState = new CharacterDefaultState(this, _characterMotor.Value, _attackHandler.Value, _lootingManager, _craftingManager, _buildingManager, _toolsManager);
        LootingState = new CharacterLootingState(this, _characterMotor.Value, _attackHandler.Value, _lootingManager, _craftingManager, _buildingManager, _toolsManager);
        CraftingState = new CharacterCraftingState(this, _characterMotor.Value, _attackHandler.Value, _lootingManager, _craftingManager, _buildingManager, _toolsManager);
        BuildingState = new CharacterBuildingState(this, _characterMotor.Value, _attackHandler.Value, _lootingManager, _craftingManager, _buildingManager, _toolsManager);
        PlacingBuildingState = new CharacterPlacingBuildableState(this, _characterMotor.Value, _attackHandler.Value, _lootingManager, _craftingManager, _buildingManager, _toolsManager, _interactionsManager);
    }
}

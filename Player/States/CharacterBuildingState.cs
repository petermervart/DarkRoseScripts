using System.Collections;
using UnityEngine;

public class CharacterBuildingState : CharacterState
{
    private ICharacterMotor _characterMotor;
    private LootingManager _lootingManager;
    private CraftingManager _craftingManager;
    private BuildingManager _buildingManager;
    private ICharacterAttackHandler _attackHandler;
    private ToolsManager _toolsManager;

    private bool _shouldSwitchToDefault = false;
    private bool _shouldSwitchPlacingBuildable = false;

    public CharacterBuildingState(Character character, ICharacterMotor characterMotor, ICharacterAttackHandler attackHandler, LootingManager lootingManager, CraftingManager craftingManager, BuildingManager buildingManager, ToolsManager toolsManager) :
    base(character)
    {
        _characterMotor = characterMotor;
        _attackHandler = attackHandler;
        _lootingManager = lootingManager;
        _craftingManager = craftingManager;
        _buildingManager = buildingManager;
        _toolsManager = toolsManager;
    }

    protected override void OnEnter()
    {
        _shouldSwitchToDefault = false;
        _shouldSwitchPlacingBuildable = false;

        _characterMotor.SetCursorLock(false);
        _characterMotor.SetLookLock(true);
        _characterMotor.SetMovementLock(true);

        _lootingManager.SetLock(true);
        _craftingManager.SetLock(true);
        _buildingManager.SetLock(false);
        _attackHandler.SetLock(true);
        _toolsManager.SetLock(true);

        _buildingManager.BuildingToggled += OnBuildingToggled;
        _buildingManager.OnPlacingBuildable += OnPlacingBuildable;
    }

    protected void OnBuildingToggled(bool isOpened)
    {
        if (!isOpened)
            _shouldSwitchToDefault = true;
    }

    protected void OnPlacingBuildable()
    {
        _shouldSwitchPlacingBuildable = true;
    }

    protected override IState OnProcessState()
    {
        if (_shouldSwitchToDefault)
            return _character.DefaultState;

        if (_shouldSwitchPlacingBuildable)
            return _character.PlacingBuildingState;

        return this;
    }

    protected override void OnExit()
    {
        _buildingManager.BuildingToggled -= OnBuildingToggled;
        _buildingManager.OnPlacingBuildable -= OnPlacingBuildable;
    }
}

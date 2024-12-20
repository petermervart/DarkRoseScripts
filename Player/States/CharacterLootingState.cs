using System.Collections;
using UnityEngine;

public class CharacterLootingState : CharacterState
{
    private ICharacterMotor _characterMotor;
    private LootingManager _lootingManager;
    private CraftingManager _craftingManager;
    private BuildingManager _buildingManager;
    private ICharacterAttackHandler _attackHandler;
    private ToolsManager _toolsManager;

    private bool _shouldSwitchToDefault = false;

    public CharacterLootingState(Character character, ICharacterMotor characterMotor, ICharacterAttackHandler attackHandler, LootingManager lootingManager, CraftingManager craftingManager, BuildingManager buildingManager, ToolsManager toolsManager) :
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

        _characterMotor.SetCursorLock(true);
        _characterMotor.SetLookLock(true);
        _characterMotor.SetMovementLock(true);

        _craftingManager.SetLock(true);
        _buildingManager.SetLock(true);
        _attackHandler.SetLock(true);
        _toolsManager.SetLock(true);

        _lootingManager.LootingToggled += OnLootingToggled;
    }

    protected void OnLootingToggled(bool isOpened)
    {
        if (!isOpened)
            _shouldSwitchToDefault = true;
    }

    protected override IState OnProcessState()
    {
        
        if (_shouldSwitchToDefault)
            return _character.DefaultState;

        return this;
    }

    protected override void OnExit()
    {
        _lootingManager.LootingToggled -= OnLootingToggled;

        _characterMotor.SetCursorLock(false);
        _characterMotor.SetLookLock(false);
        _characterMotor.SetMovementLock(false);

        _craftingManager.SetLock(false);
        _buildingManager.SetLock(false);
        _attackHandler.SetLock(false);
        _toolsManager.SetLock(false);
    }
}

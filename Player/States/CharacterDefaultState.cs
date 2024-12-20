using System.Collections;
using UnityEngine;

public class CharacterDefaultState : CharacterState
{
    private ICharacterMotor _characterMotor;
    private ICharacterAttackHandler _attackHandler;
    private LootingManager _lootingManager;
    private CraftingManager _craftingManager;
    private BuildingManager _buildingManager;
    private ToolsManager _toolsManager;

    private bool _shouldSwitchToLooting = false;
    private bool _shouldSwitchToBuilding = false;
    private bool _shouldSwitchToCrafting = false;

    public CharacterDefaultState(Character character, ICharacterMotor characterMotor, ICharacterAttackHandler attackHandler, LootingManager lootingManager, CraftingManager craftingManager, BuildingManager buildingManager, ToolsManager toolsManager) :
    base(character)
    {
        _characterMotor = characterMotor;
        _lootingManager = lootingManager;
        _craftingManager = craftingManager;
        _buildingManager = buildingManager;
        _attackHandler = attackHandler;
        _toolsManager = toolsManager;
    }

    protected override void OnEnter()
    {
        _shouldSwitchToLooting = false;
        _shouldSwitchToBuilding = false;
        _shouldSwitchToCrafting = false;

        _characterMotor.SetCursorLock(true);
        _characterMotor.SetLookLock(false);
        _characterMotor.SetMovementLock(false);

        _lootingManager.SetLock(false);
        _craftingManager.SetLock(false);
        _buildingManager.SetLock(false);
        _toolsManager.SetLock(false);
        _attackHandler.SetLock(false);

        _lootingManager.LootingToggled += OnLootingToggled;
        _craftingManager.CraftingToggled += OnCraftingToggled;
        _buildingManager.BuildingToggled += OnBuildingToggled;
    }

    protected void OnLootingToggled(bool isOpened)
    {
        if (isOpened)
            _shouldSwitchToLooting = true;
    }

    protected void OnCraftingToggled(bool isOpened)
    {
        if (isOpened)
            _shouldSwitchToCrafting = true;
    }

    protected void OnBuildingToggled(bool isOpened)
    {
        if (isOpened)
            _shouldSwitchToBuilding = true;
    }

    protected override IState OnProcessState()
    {
        if (_shouldSwitchToLooting)
            return _character.LootingState;

        if (_shouldSwitchToCrafting)
            return _character.CraftingState;

        if (_shouldSwitchToBuilding)
            return _character.BuildingState;

        return this;
    }

    protected override void OnExit()
    {
        _lootingManager.LootingToggled -= OnLootingToggled;
        _craftingManager.CraftingToggled -= OnCraftingToggled;
        _buildingManager.BuildingToggled -= OnBuildingToggled;
    }

}

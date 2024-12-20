using System.Collections;
using UnityEngine;

public class CharacterPlacingBuildableState : CharacterState
{
    private ICharacterMotor _characterMotor;
    private LootingManager _lootingManager;
    private CraftingManager _craftingManager;
    private BuildingManager _buildingManager;
    private ICharacterAttackHandler _attackHandler;
    private ToolsManager _toolsManager;
    private InteractionsManager _interactionsManager;

    private bool _shouldSwitchToBuild = false;

    public CharacterPlacingBuildableState(Character character, ICharacterMotor characterMotor, ICharacterAttackHandler attackHandler, LootingManager lootingManager, CraftingManager craftingManager, BuildingManager buildingManager, ToolsManager toolsManager, InteractionsManager interactionsManager) :
    base(character)
    {
        _characterMotor = characterMotor;
        _attackHandler = attackHandler;
        _lootingManager = lootingManager;
        _craftingManager = craftingManager;
        _buildingManager = buildingManager;
        _toolsManager = toolsManager;
        _interactionsManager = interactionsManager;
    }

    protected override void OnEnter()
    {
        _shouldSwitchToBuild = false;

        _interactionsManager.UseBuilding();

        _characterMotor.SetCursorLock(true);
        _characterMotor.SetLookLock(false);
        _characterMotor.SetMovementLock(false);

        _lootingManager.SetLock(true);
        _craftingManager.SetLock(true);
        _buildingManager.SetLock(false);
        _attackHandler.SetLock(true);
        _toolsManager.SetLock(true);

        _buildingManager.BuildingToggled += OnBuildingToggled;
    }

    protected void OnBuildingToggled(bool isOpened)
    {
        if (isOpened)
            _shouldSwitchToBuild = true;
    }

    protected override IState OnProcessState()
    {
        if (_shouldSwitchToBuild)
            return _character.BuildingState;

        return this;
    }

    protected override void OnExit()
    {
        _buildingManager.BuildingToggled -= OnBuildingToggled;
        _interactionsManager.UseDefault();
    }
}

using UnityEngine;
using AYellowpaper;

public class BuildingInteractionsHandler : MonoBehaviour, IInteractionsHandler
{
    [SerializeField]
    private InputManager _inputManager;

    [SerializeField]
    private Transform _playerCameraTransform;

    [SerializeField]
    private CrosshairHelperUI _crosshairHelper;

    [SerializeField]
    private float _checkDistance;

    [SerializeField]
    private InterfaceReference<IBuildingHandler> _buildingHandler;

    [SerializeField]
    private LayerMask _layer = ~0;

    private BuildingInputHandler _buildingInput;

    private ITrapManager _currentTarget = null;

    private void Start()
    {
        _buildingInput = _inputManager.GetInputHandler<BuildingInputHandler>(EInputCategory.Building);
        _buildingInput.OnPlacedBuild += PlaceBuild;
    }

    public void PlaceBuild()
    {
        if (_currentTarget == null)
            return;

        _crosshairHelper.Close();

        _buildingHandler.Value.OnBuild(_currentTarget);
    }

    public void CheckForTargets()
    {
        if (Physics.Raycast(_playerCameraTransform.position, _playerCameraTransform.forward, out RaycastHit hitResult, _checkDistance, _layer))
        {
            ITrapManager hitTrapManager = hitResult.collider.GetComponent<ITrapManager>();

            if (hitTrapManager != null)
            {
                if (_currentTarget == hitTrapManager)
                    return;

                _currentTarget = hitTrapManager;

                TextHelperConfigSO textConfig = hitTrapManager.LookedAt();

                if (textConfig == null) // could be that nothing will be shown
                {
                    _crosshairHelper.Close();
                    return;
                }

                _crosshairHelper.SetHelperText(textConfig.Text, textConfig.TextColor);

                return;
            }
        }

        CheckIfChanged();
    }

    private void CheckIfChanged()
    {
        if (_currentTarget == null)
            return;

        _currentTarget = null;
        _crosshairHelper.Close();
    }
}
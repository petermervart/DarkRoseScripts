using UnityEngine;
using AYellowpaper;

public class DefaultInteractionsHandler : MonoBehaviour, IInteractionsHandler
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
    private InterfaceReference<ILootingHandler> _lootingHandler;

    [SerializeField]
    private LayerMask _layer = ~0;

    private InteractionsInputHandler _interactionsInput;

    private GameObject _currentTarget = null;

    private void Start()
    {
        _interactionsInput = _inputManager.GetInputHandler<InteractionsInputHandler>(EInputCategory.Interacting);
        _interactionsInput.OnInteract += Interact;
    }

    public void Interact()
    {
        if (_currentTarget == null)
            return;

        _crosshairHelper.Close();

        if (_currentTarget.TryGetComponent( out ILootable lootable)) // check if its lootable
        {
            _lootingHandler.Value.LootedObject(lootable);
        }
        // add more types of interactables later
    }

    public void CheckForTargets()
    {
        if (Physics.Raycast(_playerCameraTransform.position, _playerCameraTransform.forward, out RaycastHit hitResult, _checkDistance, _layer))
        {
            IInteractable hitInteractable = hitResult.collider.GetComponent<IInteractable>();
            GameObject newTarget = null;

            if (hitInteractable != null) // this looks bad.. might find better solution in the future since we cant get gameobject from interface... its needed because different LODs have different colliders and we have to check parent too
            {
                newTarget = hitResult.collider.gameObject;
            }
            else if (hitResult.collider.transform.parent != null)
            {
                hitInteractable = hitResult.collider.transform.parent.GetComponent<IInteractable>();
                newTarget = hitInteractable != null
                    ? hitResult.collider.transform.parent.gameObject
                    : null;
            }

            if (hitInteractable != null)
            {
                if (_currentTarget == newTarget)
                    return;

                _currentTarget = newTarget;

                TextHelperConfigSO textConfig = hitInteractable.LookedAt();

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
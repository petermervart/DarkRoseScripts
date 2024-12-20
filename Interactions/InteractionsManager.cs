using System.Collections;
using UnityEngine;
using AYellowpaper;

public class InteractionsManager : MonoBehaviour
{
    [SerializeField]
    private float _checkRate; // Check interval in seconds.

    [SerializeField]
    private InterfaceReference<IInteractionsHandler> _defaultInteractionsHandler;

    [SerializeField]
    private InterfaceReference<IInteractionsHandler> _buildingInteractionsHandler;

    public bool ShouldCheck { get; set; }

    private IInteractionsHandler _currentInteractionsHandler;
    private float _nextCheckTime;

    private void Start()
    {
        _currentInteractionsHandler = _defaultInteractionsHandler.Value;
        ShouldCheck = true;
        _nextCheckTime = Time.time;
    }

    public void UseBuilding()
    {
        _currentInteractionsHandler = _buildingInteractionsHandler.Value;
    }

    public void UseDefault()
    {
        _currentInteractionsHandler = _defaultInteractionsHandler.Value;
    }

    private void Update()
    {
        if (ShouldCheck && Time.time >= _nextCheckTime)
        {
            _currentInteractionsHandler.CheckForTargets();
            _nextCheckTime = Time.time + _checkRate;
        }
    }
}
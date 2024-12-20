using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;

public class TrapManager : MonoBehaviour, ITrapManager
{
    [SerializeField]
    private TrapManagerConfigSO _trapManagerConfig;

    [SerializeReference]
    private readonly Dictionary<Guid, ITrap> _trapsDictionary = new Dictionary<Guid, ITrap>();

    private ITrap _currentTrap = null;

    public bool IsOccupied => _currentTrap != null;

    private void Awake()
    {
        ITrap[] traps = GetComponentsInChildren<ITrap>(true);

        for (int i = 0; i < traps.Length; i++)
        {
            _trapsDictionary.Add(traps[i].InventoryItem.ItemID, traps[i]);
        }
    }

    // Managing traps, only one trap per manager (traps are preplaced in the world on doors mainly for good positioning and also to be able to cache sources from world when NavMesh baking (barricade))
    public bool OnTrapPlaced(Guid trapInventoryID, out TimedTextHelperConfigSO textHelper)
    {
        if (_currentTrap != null)
        {
            textHelper = _trapManagerConfig.TriedToPlaceOnOccupied;
            return false;
        }

        if (_trapsDictionary.TryGetValue(trapInventoryID, out ITrap trap))
        {
            _currentTrap = trap;
            trap.Placed();
            trap.OnTrapRemoved += OnTrapRemoved;
            textHelper = _trapManagerConfig.PlacedTrap;
            return true;
        }
        else
        {
            DebugUtils.LogError("Trap not in the dictionary");
            textHelper = _trapManagerConfig.CantPlaceHere;
            return false;
        }
    }

    public TextHelperConfigSO LookedAt()
    {
        if (_currentTrap != null)
            return _trapManagerConfig.AlreadyOccupied;
        else
            return _trapManagerConfig.NotOccupied;
    }

    public void OnTrapRemoved()
    {
        _currentTrap = null;
    }
}

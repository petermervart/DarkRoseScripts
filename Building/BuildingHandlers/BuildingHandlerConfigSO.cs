using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building/Building Handler Config", fileName = "BuildingHandlerConfig")]
public class BuildingHandlerConfigSO : ScriptableObject
{
    [Header("Building")]
    public List<BuildableItemConfigSO> BuildableItems;

    [Header("UI")]
    public TimedTextHelperConfigSO CantAffordTextHelper;
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Looting/Lootable Object UI Config", fileName = "LootableObjectUIConfig")]
public class LootableObjectUIConfigSO : ScriptableObject
{
    [Header("Interactions Handling")]
    public TextHelperConfigSO CanBeLooted;

    public TimedTextHelperConfigSO AlreadyLooted;

    public TimedTextHelperConfigSO NoLootInObject;
}

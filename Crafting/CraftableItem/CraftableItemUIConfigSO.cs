using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Crafting/Craftable Item UI Config", fileName = "CraftableItemUIConfig")]
public class CraftableItemUIConfigSO : ScriptableObject
{
    [Header("UI")]
    public Color NormalColor;
    public Color CantAffordColor;
    public Image.FillMethod ImageFillMethod;
}

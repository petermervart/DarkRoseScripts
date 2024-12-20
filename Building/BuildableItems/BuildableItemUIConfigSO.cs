using UnityEngine;

[CreateAssetMenu(menuName = "Building/Buildable Item UI Config", fileName = "BuildableItemUIConfig")]
public class BuildableItemUIConfigSO : ScriptableObject
{
    [Header("UI")]
    public Color NormalColor;
    public Color CantBuildColor;
}

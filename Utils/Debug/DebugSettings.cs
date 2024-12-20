using UnityEngine;

[CreateAssetMenu(menuName = "Debug/DebugSettings", fileName = "DebugSettings")]
public class DebugSettings : ScriptableObject
{
    [Header("Logging")]
    [Tooltip("Enable or disable normal log messages.")]
    public bool ShouldLogNormal = true;

    [Tooltip("Enable or disable warning log messages.")]
    public bool ShouldLogWarning = true;

    [Tooltip("Enable or disable error log messages.")]
    public bool ShouldLogError = true;
}

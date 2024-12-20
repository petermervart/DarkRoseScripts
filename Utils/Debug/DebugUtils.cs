using UnityEngine;

public static class DebugUtils
{
    [System.Diagnostics.Conditional("ENABLE_LOG")]
    static public void Log(object message)
    {
        if (DebugSettingsManager.DebugSettings != null && DebugSettingsManager.DebugSettings.ShouldLogNormal)
        {
            DebugUtils.Log(message);
        }
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    static public void LogWarning(object message)
    {
        if (DebugSettingsManager.DebugSettings != null && DebugSettingsManager.DebugSettings.ShouldLogWarning)
        {
            DebugUtils.LogWarning(message);
        }
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    static public void LogError(object message)
    {
        if (DebugSettingsManager.DebugSettings != null && DebugSettingsManager.DebugSettings.ShouldLogError)
        {
            DebugUtils.LogError(message);
        }
    }
}

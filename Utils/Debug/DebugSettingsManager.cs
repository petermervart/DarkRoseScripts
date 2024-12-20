using UnityEngine;

public class DebugSettingsManager : MonoBehaviour
{
    public static DebugSettings DebugSettings;

    [SerializeField] private DebugSettings DebugSettingsAsset; // this could be loaded in a file in the future but its ok for now

    private void Awake()
    {
        // Assign the ScriptableObject to the static field
        DebugSettings = DebugSettingsAsset;
    }
}
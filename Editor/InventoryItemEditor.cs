#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InventoryItemConfigSO))]
public class InventoryItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        InventoryItemConfigSO item = (InventoryItemConfigSO)target;

        // Draw default fields (Name, IconSprite, MaxAmount)
        DrawDefaultInspector();

        // Make the GUID read-only in the Inspector
        GUI.enabled = false; // Disable editing
        EditorGUILayout.TextField("Item ID", item.ItemID.ToString());
        GUI.enabled = true; // Re-enable editing
    }
}
#endif
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Options/Graphics Options Config", fileName = "GraphicsOptionsConfig")]
public class GraphicsOptionsConfigSO : ScriptableObject
{
    [Header("Graphics")]
    public int CurrentGraphicsSettings = 0;
}

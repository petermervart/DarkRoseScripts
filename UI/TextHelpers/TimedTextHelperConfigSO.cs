using UnityEngine;

[CreateAssetMenu(menuName = "UI/Timed Text Helper Config", fileName = "TimedTextHelperConfig")]
public class TimedTextHelperConfigSO : ScriptableObject
{
    public string Text;
    public Color TextColor;
    public float DurationToShow;
}

using System;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Options Config", fileName = "OptionsConfig")]
public class OptionsConfigSO : ScriptableObject
{
    [Header("Sound")]
    public float CurrentGameplayVolume;
    public float CurrentMusicVolume;

    [Header("Graphics")]
    public int CurrentGraphicsSettings = 0;

    public void OnChangedSettings(int newSettingsIndex)
    {
        QualitySettings.SetQualityLevel(newSettingsIndex, true);
        CurrentGraphicsSettings = newSettingsIndex;
    }
}

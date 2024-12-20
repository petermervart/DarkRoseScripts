using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audio/Volume Options Config", fileName = "VolumeOptionsConfig")]
public class VolumeOptionsConfigSO : ScriptableObject
{
    public EAudioSourceType AudioType;
    public float Volume;
    public AudioMixerGroup LinkedAudioGroup;
    public string VolumeParamName;
}

using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Audio Options/Audio Options Config", fileName = "AudioOptionsConfig")]
public class AudioOptionsConfigSO : ScriptableObject
{
    public VolumeOptionsConfigSO[] VolumeOptions;
}

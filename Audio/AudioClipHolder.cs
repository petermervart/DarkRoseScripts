using UnityEngine;

[System.Serializable]
public class AudioClipHolder
{
    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume;

    public AudioClipHolder(AudioClip clip, float volume)
    {
        Clip = clip;
        Volume = volume;
    }
}

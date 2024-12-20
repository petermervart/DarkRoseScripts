using UnityEngine;
using System.Collections.Generic;

public static class AudioUtils
{
    public static void PlayOneShot(AudioSource audioSource, AudioClipHolder audioClip)
    {
        if (audioSource == null || audioClip == null)
        {
            DebugUtils.LogWarning("AudioUtils: Something Went Wrong... Not playing audio clip");
        }

        audioSource.PlayOneShot(audioClip.Clip, audioClip.Volume);
    }

    public static void PlayOneShotRandomAudioClip(AudioSource audioSource, AudioClipHolder[] audioClips)
    {
        if(audioSource == null || audioClips == null || audioClips.Length == 0)
        {
            DebugUtils.LogWarning("AudioUtils: Something Went Wrong... Not playing audio clip");
        }

        AudioClipHolder randomClip = audioClips[Random.Range(0, audioClips.Length)];

        audioSource.PlayOneShot(randomClip.Clip, randomClip.Volume);
    }

    public static void PlayClipAtPoint(AudioClipHolder audioClip, Vector3 position)
    {
        if (audioClip.Clip == null)
        {
            DebugUtils.LogWarning("AudioUtils: Attempted to play a null AudioClip.");
            return;
        }

        AudioSource.PlayClipAtPoint(audioClip.Clip, position, audioClip.Volume);
    }

    public static void PlayClipOnLoop(bool shouldPlay, AudioSource audioSource, AudioClipHolder[] audioClips)
    {
        audioSource.loop = shouldPlay;

        if (shouldPlay)
        {
            AudioClipHolder randomClip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.clip = randomClip.Clip;
            audioSource.volume = randomClip.Volume;
            audioSource.Play();
        }
        else
        {
            audioSource.volume = 1.0f;
            audioSource.Stop();
        }
    }
}
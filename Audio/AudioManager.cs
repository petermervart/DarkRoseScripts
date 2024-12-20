using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum EAudioSourceType
{
    Gameplay = 0,
    Music = 1,
    VFX = 2
}

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private GameStateManager _gameStateManager;

    [SerializeField]
    private AudioOptionsConfigSO _audioOptionsConfig;

    [SerializeField]
    private AudioOptionsUI _audioOptionsUI;

    [SerializeField]
    private AudioMixer _audioMixer;

    private Dictionary<EAudioSourceType, VolumeOptionsConfigSO> _audioOptionsDict; // this let us add multiple audio types and handle their volume and even make parent volume settings

    private void Awake()
    {
        _audioOptionsDict = new Dictionary<EAudioSourceType, VolumeOptionsConfigSO>();

        for (int i = 0; i < _audioOptionsConfig.VolumeOptions.Length; i++) // has to do this here to be able to set mixer type
        {
            _audioOptionsDict[_audioOptionsConfig.VolumeOptions[i].AudioType] = _audioOptionsConfig.VolumeOptions[i];
        }
    }

    private void Start()
    {
        for (int i = 0; i < _audioOptionsConfig.VolumeOptions.Length; i++) // has to do this here because mixer volume can only be applied after Awake
        {
            SetMixerVolume(_audioOptionsConfig.VolumeOptions[i].VolumeParamName, _audioOptionsConfig.VolumeOptions[i].Volume);
        }

        _audioOptionsUI.OnSliderValueChanged += VolumeChanged;
        _gameStateManager.OnGamePause += OnPause;
    }

    public float GetVolumeValueWithType(EAudioSourceType type)
    {
        return _audioOptionsDict[type].Volume;
    }

    public AudioMixerGroup GetAudioGroupWithType(EAudioSourceType type)
    {
        return _audioOptionsDict[type].LinkedAudioGroup;
    }

    public void VolumeChanged(EAudioSourceType type, float newVolume)
    {
        DebugUtils.Log("Changed Volume");
        _audioOptionsDict[type].Volume = newVolume;
        SetMixerVolume(_audioOptionsDict[type].VolumeParamName, newVolume);
    }

    private void SetMixerVolume(string parameterName, float volume)
    {
        // Convert linear volume (0.0 to 1.0) to decibels for the AudioMixer (-80dB to 0dB)
        float dbVolume = Mathf.Log10(Mathf.Clamp(volume, 0.00001f, 1f)) * 20;
        _audioMixer.SetFloat(parameterName, dbVolume);
    }

    public void OnPause(bool isPaused)
    {
        AudioListener.pause = isPaused;
    }
}

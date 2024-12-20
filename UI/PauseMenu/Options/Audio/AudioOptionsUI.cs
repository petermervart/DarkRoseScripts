using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct AudioSliderHolder
{
    public EAudioSourceType AudioType;
    public Slider LinkedSlider;
}

public class AudioOptionsUI : MonoBehaviour
{
    [SerializeField]
    private AudioManager _audioManager;

    [SerializeField]
    private AudioSliderHolder[] _audioSliders;

    public event Action<EAudioSourceType, float> OnSliderValueChanged;

    private Dictionary<EAudioSourceType, Slider> _audioSlidersDict;

    private void Awake()
    {
        _audioSlidersDict = new ();

        for (int i = 0; i < _audioSliders.Length; i++)
        {
            _audioSlidersDict[_audioSliders[i].AudioType] = _audioSliders[i].LinkedSlider;

            int cachedIndex = i; // have to do this in order to have correct index upon invoking the event

            _audioSliders[cachedIndex].LinkedSlider.onValueChanged.AddListener(value =>
            {
                OnSliderValueChanged?.Invoke(_audioSliders[cachedIndex].AudioType, value);
            });

            _audioSliders[i].LinkedSlider.value = _audioManager.GetVolumeValueWithType(_audioSliders[i].AudioType);
        }
    }
}
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SlowDownTrapAudioHandler : MonoBehaviour, ISlowDownTrapAudioHandler
{
    [SerializeField]
    private AudioClipHolder[] _enabledTrapSounds;

    private ISlowDownTrap _slowDownTrap;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        InitAudioHandler();
    }

    private void InitAudioHandler()
    {
        _slowDownTrap = GetComponentInParent<ISlowDownTrap>();

        if (_slowDownTrap != null)
        {
            _slowDownTrap.OnTrapChangedEnable += OnTrapChangedEnable;
        }
    }

    public void OnTrapChangedEnable(bool shouldPlay)
    {
        AudioUtils.PlayClipOnLoop(shouldPlay, _audioSource, _enabledTrapSounds);
    }
}
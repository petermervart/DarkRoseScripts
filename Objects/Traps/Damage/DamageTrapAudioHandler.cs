using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DamageTrapAudioHandler : MonoBehaviour, IDamageTrapAudioHandler
{
    [SerializeField]
    private AudioClipHolder[] _activateTrapSounds;

    [SerializeField]
    private AudioClipHolder[] _hitTrapSounds;

    private IDamageTrap _linkedDamageTrap;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        InitAudioHandler();
    }

    private void InitAudioHandler()
    {
        _linkedDamageTrap = GetComponentInParent<IDamageTrap>();

        if (_linkedDamageTrap != null)
        {
            _linkedDamageTrap.OnTrapActivated += OnTrapActivated;
            _linkedDamageTrap.OnTrapHit += OnTrapHit;
        }
    }

    public void OnTrapActivated()
    {
        AudioUtils.PlayOneShotRandomAudioClip(_audioSource, _activateTrapSounds);
    }

    public void OnTrapHit()
    {
        AudioUtils.PlayOneShotRandomAudioClip(_audioSource, _hitTrapSounds);
    }
}
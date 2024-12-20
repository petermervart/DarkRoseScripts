using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DestructableObstructionAudioHandler : MonoBehaviour, IDestructableObstructionAudioHandler
{
    [SerializeField]
    private AudioClipHolder[] _hitDestructableSounds;

    [SerializeField]
    private AudioClipHolder[] _damagedDestructableSounds;

    [SerializeField]
    private AudioClipHolder[] _destroyedDestructableSounds;

    private IDestructableObstruction _linkedDestructable;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        InitAudioHandler();
    }

    private void InitAudioHandler()
    {
        _linkedDestructable = GetComponentInParent<IDestructableObstruction>();

        if (_linkedDestructable != null)
        {
            _linkedDestructable.OnGotDamaged += OnDestructableDamaged;
            _linkedDestructable.OnGotHit += OnDestructableHit;
            _linkedDestructable.OnHealthDepleted += OnDestructableDestroyed;
        }
    }

    public void OnDestructableHit()
    {
        AudioUtils.PlayOneShotRandomAudioClip(_audioSource, _hitDestructableSounds);
    }

    public void OnDestructableDamaged()
    {
        AudioUtils.PlayOneShotRandomAudioClip(_audioSource, _damagedDestructableSounds);
    }

    public void OnDestructableDestroyed()
    {
        AudioUtils.PlayOneShotRandomAudioClip(_audioSource, _destroyedDestructableSounds);
    }
}
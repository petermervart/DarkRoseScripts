using System.Collections.Generic;
using UnityEngine;

// all character sound here for now... should divide it more in the future when adding more content (mainly weapon and tool managers)
[RequireComponent(typeof(AudioSource))]
public class CharacterAudioHandler : MonoBehaviour, ICharacterAudioHandler
{
    [SerializeField]
    private AudioClipHolder[] hitGroundSounds;

    [SerializeField]
    private AudioClipHolder[] footStepsSoundsGround;

    [SerializeField]
    private AudioClipHolder[] footStepsSoundsConcrete;

    [SerializeField]
    private AudioClipHolder[] footStepsSoundsWood;

    [SerializeField]
    private AudioClipHolder[] gotHurtSounds;

    [SerializeField]
    private AudioClipHolder[] hitSomethingSounds;

    [SerializeField]
    private AudioClipHolder[] hitNothingSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnHitGround()
    {
        AudioUtils.PlayOneShotRandomAudioClip(_audioSource, hitGroundSounds);
    }

    public void OnHit(bool hitSomething)
    {
        if(hitSomething)
            AudioUtils.PlayOneShotRandomAudioClip(_audioSource, hitSomethingSounds);
        else
            AudioUtils.PlayOneShotRandomAudioClip(_audioSource, hitNothingSound);
    }

    public void OnGotHurt()
    {
        AudioUtils.PlayOneShotRandomAudioClip(_audioSource, gotHurtSounds);
    }

    public void OnFootSteps(ESurfaceType groundMaterial) // probably should change this to dict in the future for more robust solution
    {
        switch (groundMaterial)
        {
            case ESurfaceType.Concrete:
                AudioUtils.PlayOneShotRandomAudioClip(_audioSource, footStepsSoundsConcrete);
                break;
            case ESurfaceType.Wood:
                AudioUtils.PlayOneShotRandomAudioClip(_audioSource, footStepsSoundsWood);
                break;
            default:
                AudioUtils.PlayOneShotRandomAudioClip(_audioSource, footStepsSoundsGround);
                break;
        }
    }
}

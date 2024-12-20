using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class LevelMusicHandler : MonoBehaviour
{
    [SerializeField]
    private AudioClipHolder[] _levelMusicDay;

    [SerializeField]
    private AudioClipHolder[] _levelMusicNight;

    [SerializeField]
    private AudioClipHolder[] _nightStartedClips;

    private bool _isSourceStopped = false;

    private bool _isNight = false;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.ignoreListenerPause = true;
    }

    private void Start()
    {
        PlayMusic();
    }

    public void NightStarted()
    {
        StartCoroutine(ChangedToNight());
    }

    protected IEnumerator ChangedToNight()
    {
        _audioSource.Stop();

        _isSourceStopped = true;

        AudioClipHolder randomClip = _nightStartedClips[Random.Range(0, _nightStartedClips.Length)];

        AudioUtils.PlayOneShot(_audioSource ,randomClip);

        yield return new WaitForSeconds(randomClip.Clip.length);

        _isNight = true;

        PlayMusic();

        _isSourceStopped = false;
    }

    protected void Update()
    {
        if (!_isSourceStopped && !_audioSource.isPlaying)
        {
            PlayMusic();
        }
    }

    public void PlayMusic()
    {
        if (_isNight)
            AudioUtils.PlayClipOnLoop(true, _audioSource, _levelMusicNight);
        else
            AudioUtils.PlayClipOnLoop(true, _audioSource, _levelMusicDay);
    }

    public void OnClipEnded()
    {
        PlayMusic();
    }
}
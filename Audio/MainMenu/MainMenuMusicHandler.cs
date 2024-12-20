using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainMenuMusicHandler : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _mainMenuMusic;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        _audioSource.clip = _mainMenuMusic[Random.Range(0, _mainMenuMusic.Count)];
        _audioSource.Play();
        Invoke(nameof(OnClipEnded), _audioSource.clip.length);
    }

    public void OnClipEnded()
    {
        PlayMusic();
    }
}
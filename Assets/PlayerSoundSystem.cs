using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundSystem : MonoBehaviour
{
    [SerializeField] AudioClip _playerHurtSound;
    [SerializeField] AudioClip _playerAttackAudioClip;

    AudioSource[] _audioSources;

    private void Awake()
    {
        _audioSources = GetComponents<AudioSource>();
    }

    public void PlayPlayerIsHitSound()
    {
        _audioSources[0].volume = 0.125f;
        _audioSources[0].clip = _playerHurtSound;
        _audioSources[0].Play();
    }

    public void PlayPlayerAttackSound()
    {
        _audioSources[1].volume = 0.125f;
        _audioSources[1].clip = _playerAttackAudioClip;
        _audioSources[1].Play();
    }

}

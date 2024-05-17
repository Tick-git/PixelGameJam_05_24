using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterdropSoundSystem : MonoBehaviour
{
    [SerializeField] AudioClip _waterdropCollectSound;
    [SerializeField] float _volume = 0.1f;

    Queue<AudioSource> _audioSources;

    private void Awake()
    {
        _audioSources = new Queue<AudioSource>();

        foreach(AudioSource audioSource in GetComponents<AudioSource>())
        {
            audioSource.clip = _waterdropCollectSound;
            audioSource.volume = _volume;
            _audioSources.Enqueue(audioSource);
        }    
    }

    public void PlayWaterdropCollectedSound()
    {
        if (_audioSources.Count <= 0) return;

        AudioSource audioSource = _audioSources.Dequeue();
        audioSource.Play();

        StartCoroutine(WaitForAudioClipEnd(audioSource));

    }

    private IEnumerator WaitForAudioClipEnd(AudioSource audioSource)
    {
        while(audioSource.isPlaying)
        {
            yield return null;
        }

        _audioSources.Enqueue(audioSource);
    }
}

using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    AudioSource _audioSource;
    Coroutine _isPlayingCoroutine;

    public SoundData SoundData { get; private set; }

    private void Awake()
    {
        _audioSource = gameObject.GetOrAddComponent<AudioSource>();
    }

    public void Play()
    {
        if(_isPlayingCoroutine != null)
        {
            StopCoroutine(_isPlayingCoroutine);
        }

        _audioSource.Play();
        _isPlayingCoroutine = StartCoroutine(WaitForSoundToEnd());
    }

    public void Stop()
    {
        if(_isPlayingCoroutine != null)
        {
            StopCoroutine(_isPlayingCoroutine);
            _isPlayingCoroutine = null;
        }

        _audioSource.Stop();
        SoundManager.Instance.ReturnToPool(this);
    }

    private IEnumerator WaitForSoundToEnd()
    {
        yield return new WaitWhile(() => _audioSource.isPlaying);

        SoundManager.Instance.ReturnToPool(this);
    }

    public void Initialize(SoundData data)
    {
        SoundData = data;
        _audioSource.clip = data.Clip;
        _audioSource.outputAudioMixerGroup = data.MixerGroup;
        _audioSource.playOnAwake = data.PlayOnAwake;
        _audioSource.loop = data.Loop;
        _audioSource.volume = data.Volume;
        _audioSource.pitch = data.Pitch;
    }

    internal void WithRandomPitch(float min = -0.05f, float max = 0.05f)
    {
        _audioSource.pitch += UnityEngine.Random.Range(min, max);
    }
}

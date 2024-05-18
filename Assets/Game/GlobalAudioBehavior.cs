using UnityEngine;

public class GlobalAudioBehavior : MonoBehaviour
{
    AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();    
    }
}

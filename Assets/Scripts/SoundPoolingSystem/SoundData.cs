using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
[CreateAssetMenu(fileName = "SoundData", menuName = "Data/SoundData")]
public class SoundData : ScriptableObject
{
    public AudioClip Clip;
    public AudioMixerGroup MixerGroup;
    public bool Loop;
    public bool PlayOnAwake;
    public float Volume = 1f;
    public float Pitch = 1f;
}

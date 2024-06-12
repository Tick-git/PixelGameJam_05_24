using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterdropSoundSystem : MonoBehaviour
{
    [SerializeField] SoundData _soundData;


    private void Awake()
    {
   
    }

    public void PlayWaterdropCollectedSound()
    {
        SoundManager.Instance.CreateSound()
            .WithSoundData(_soundData)
            .WithRandomPitch()
            .Play();
    }

}

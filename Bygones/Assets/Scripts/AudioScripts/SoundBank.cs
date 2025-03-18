//  Author Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBank : MonoBehaviour
{

    public static SoundBank Instance { get; private set; }
    public AudioClip stepAudioGrass;
    



    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}

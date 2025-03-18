using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Footsteps_grass
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance; 
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1.0f)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
        instance.audioSource.loop = true;
    }
}

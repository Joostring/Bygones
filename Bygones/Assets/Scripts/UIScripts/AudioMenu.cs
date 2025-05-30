using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

//Made by Jennifer

public class AudioMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void SetMasterVolume(float volume)
    {
        int volumeToInt = Mathf.Clamp((int)(volume * 10), 0, 20);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFX(float volume)
    {
        int volumeToInt = Mathf.Clamp((int)(volume * 10), 0, 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void SetMusic(float volume)
    {
        int volumeToInt = Mathf.Clamp((int)(volume * 10), 0, 20);
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    CheckTerrainTexture ctt;
    public AudioSource source;
    public AudioClip[] grassClips;
    public AudioClip[] stoneClips;
    public AudioClip[] dirtClips;
    
    AudioClip previousClip;
    public void PlayFootstep()
    {
        ctt.GetTerrainTexture();
        if (ctt.textureValues[0] > 0)
        {
            source.PlayOneShot(GetClip(grassClips), ctt.textureValues[0]);
        }
        if (ctt.textureValues[1] > 0)
        {
            source.PlayOneShot(GetClip(stoneClips), ctt.textureValues[1]);
        }
        if (ctt.textureValues[2] > 0)
        {
            source.PlayOneShot(GetClip(dirtClips), ctt.textureValues[2]);
        }
        
    }
    AudioClip GetClip(AudioClip[] clipArray)
    {
        int attempts = 3;
        AudioClip selectedClip =
        clipArray[Random.Range(0, clipArray.Length - 1)];
        while (selectedClip == previousClip && attempts > 0)
        {
            selectedClip =
            clipArray[Random.Range(0, clipArray.Length - 1)];

            attempts--;
        }
        previousClip = selectedClip;
        return selectedClip;
    }


}

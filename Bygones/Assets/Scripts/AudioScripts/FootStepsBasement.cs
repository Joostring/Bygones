// Author Jonas Östring

using UnityEngine;
using UnityEngine.Audio;

public class FootStepsBasement : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] concreteClips;
   



    private AudioSource aSource;
    private TerrainDetector tDetector;

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
        tDetector = new TerrainDetector();


    }

    
    private void Step()
    {
        AudioClip clip = GetRandomClipBasement();
        aSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClipBasement()
    {
        int terrainTextureIndex = tDetector.GetActiveTerrainTextureIdx(transform.position);

        switch (terrainTextureIndex)
        {
            case 0:
               default:
                return concreteClips[UnityEngine.Random.Range(0, concreteClips.Length)];
           



        }

    }
}
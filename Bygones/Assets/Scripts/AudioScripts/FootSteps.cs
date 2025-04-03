// Author Jonas Östring

using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] grassClips;
    [SerializeField]
    private AudioClip[] stoneClips;
    [SerializeField]
    private AudioClip[] mudClips;
    

    private AudioSource audioSource;
    private TerrainDetector terrainDetector;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        terrainDetector = new TerrainDetector();
    }

    private void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);

        switch (terrainTextureIndex)
        {
            case 0:
                //return stoneClips[UnityEngine.Random.Range(0, stoneClips.Length)];
                return grassClips[UnityEngine.Random.Range(0, grassClips.Length)];
            case 1:
                //return mudClips[UnityEngine.Random.Range(0, mudClips.Length)];
                return stoneClips[UnityEngine.Random.Range(0, stoneClips.Length)];
            case 2:
             default:
                return mudClips[UnityEngine.Random.Range(0, mudClips.Length)];
            
                
        }

        }
    }
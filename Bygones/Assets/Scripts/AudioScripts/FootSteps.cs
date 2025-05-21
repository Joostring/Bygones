// Author Jonas Östring

using UnityEngine;
using UnityEngine.Audio;

public class FootSteps : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] grassClips;
    [SerializeField]
    private AudioClip[] stoneClips;
    [SerializeField]
    private AudioClip[] mudClips;
    [SerializeField]
    private AudioClip[] dirtClips;
    [SerializeField]
    private AudioClip[] floorClips;
    
  
    private AudioSource audioSource;
    private TerrainDetector terrainDetector;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        terrainDetector = new TerrainDetector();
        playerMovement = GetComponent<PlayerMovement>();
        
       
    }

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
    //    {
    //        AudioClip clip = GetRandomClip();
    //        audioSource.PlayOneShot(clip);
    //    }
    //}
    private void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);

        if (playerMovement.isPlayerMoving == true)
        {
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
                case 3:
                    return dirtClips[UnityEngine.Random.Range(0, mudClips.Length)];
                case 4:
                    return floorClips[UnityEngine.Random.Range(0, mudClips.Length)];



            }
        }
        else
        {
            return null;
        }

    }
    }
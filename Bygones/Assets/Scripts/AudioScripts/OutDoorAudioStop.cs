using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutDoorAudioStop : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !audioSource.isPlaying)
        {
            audioSource.Play();
            Debug.Log("Collision");
        }
    }
    //public GameObject audioObject;
    //void Start()
    //{
    //    audioObject = GameObject.Find("Audio Source");

    //    if (audioObject == null)
    //    {
    //        Debug.Log("There is no audiosource");
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    AudioSource source = audioObject.GetComponent<AudioSource>();

    //    if (source != null)
    //    {
    //        source.Stop();
    //    }
    //    else
    //    { 
    //        Debug.Log("There is no audiosource"); 
    //    }
    //}
}

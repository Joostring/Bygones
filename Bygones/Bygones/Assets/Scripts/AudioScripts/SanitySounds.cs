// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanitySounds : MonoBehaviour
{
    [SerializeField] AudioSource ambient;
    [SerializeField] AudioSource sanity;
    [SerializeField] LowSanityTimer timer;
    //private LowSanityTimer timer;

    private void Awake()
    {
        sanity.enabled = false;
        ambient.enabled = true;
        //ambient = GetComponent<AudioSource>();
        //sanity = GetComponent<AudioSource>();
        //timer = GetComponent<LowSanityTimer>();
    }

    private void Update()
    {
        PlaySanity();
    }

    private void PlaySanity()
    {
        
        if(timer.sanityProcentage <= 25f)
        {
            Debug.Log("Under 25");
            sanity.enabled = true;
            ambient.enabled = false;
            //sanity.Play();
            //ambient.Stop();
        }
        else
        {
            //ambient.Play();
            //sanity.Stop();
            sanity.enabled = false;
            ambient.enabled = true;
        }
    }
}

// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughTrigger : MonoBehaviour
{
    private AudioSource laughSource;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            laughSource = GameObject.Find("Laugh1").GetComponent<AudioSource>();  
            laughSource.Play();
            Destroy(this);
            
        }
        else if (other.tag == "Player")
        {
            laughSource = GameObject.Find("Laugh2").GetComponent<AudioSource>();
            laughSource.Play();
            Destroy(this);
        }
    }
}

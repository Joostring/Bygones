// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource vase;

    private void OnTriggerEnter(Collider other)
    {
        vase = GameObject.Find("BreakingVase").GetComponent<AudioSource>();

        if (other.tag == "Player")
        {
           
            Debug.Log("Collision");
            vase.Play();
            Destroy(this);
        }
    }
}

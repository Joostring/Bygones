using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource piano;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            piano = GameObject.Find("Piano").GetComponent<AudioSource>();
            piano.Play();
            Destroy(this);
        }
    }
}

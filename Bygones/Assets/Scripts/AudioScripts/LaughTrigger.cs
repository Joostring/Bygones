// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughTrigger : MonoBehaviour
{
    [SerializeField] private GameObject laugh1;
    [SerializeField] private GameObject laugh2;
    private AudioSource laughSource;

    private void Start()
    {
        laugh1.SetActive(true);
        laugh2.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && laugh1.tag == "Laugh1")
        {
            laughSource = GameObject.Find("Laugh1").GetComponent<AudioSource>();  
            laughSource.Play();
            //Destroy(this);
            //laugh1.SetActive(false);
            
        }
        else if (other.tag == "Player" && laugh2.tag == "Laugh2")
        {
            laughSource = GameObject.Find("Laugh2").GetComponent<AudioSource>();
            laughSource.Play();
            //Destroy(this);
            //laugh2.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && laugh1.tag == "Laugh1")
        {
            laugh1.SetActive(false);
        }
        else if (other.tag == "Player" && laugh2.tag == "Laugh2")
        {
            laugh2.SetActive(false);
        }
    }
}

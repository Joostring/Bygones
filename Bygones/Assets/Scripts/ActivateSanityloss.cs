using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSanityloss : MonoBehaviour
{
    [SerializeField] private LowSanityTimer sanityTimer;


    //private void Start()
    //{
    //    sanityTimer.SetActive(false);
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (sanityTimer != null)
        {
            if (other.CompareTag("Player"))
            {
                sanityTimer.isSanityDraing = true;
            }
        }
    }
}

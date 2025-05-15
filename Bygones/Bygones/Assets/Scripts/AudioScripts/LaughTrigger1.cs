// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughTrigger1 : MonoBehaviour
{
    [SerializeField] private GameObject laughObject;
    
    private void Start()
    {
        laughObject.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

           if (laughObject.CompareTag("Laugh1") && laughObject != null)
           {

                laughObject.SetActive(true);
                
            }
            
        }
        
    }
   
}

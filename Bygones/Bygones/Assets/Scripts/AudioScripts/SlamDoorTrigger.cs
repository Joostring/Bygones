// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamDoorTrigger : MonoBehaviour
{
     private SingleDoorController controller;
    
    private void OnTriggerEnter(Collider other)
    {
        controller = GameObject.Find("SlamDoor").GetComponent<SingleDoorController>();

        if (controller.doorOpen && other.tag == "Player")
        {          
            Debug.Log("Collision");
            controller.SlamAnimation();
            Destroy(this);           
        }
    }
}

// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject leverText;
    [SerializeField] private KeyCode pullKey = KeyCode.E;
    [SerializeField] private LeverController leverController;
    [SerializeField] private BasementDoorController basementDoorController;


    public bool isPulled;
    private bool inReach;

    private void Start()
    {
        leverText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = true;
            leverText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = false;
            leverText.SetActive(false);
        }
    }

    private void Update()
    {
        
    
        if (leverController != null && inReach && Input.GetKeyDown(pullKey))
        {
            isPulled = !isPulled; // Toggle the state
            leverController.PlayAnimation();
            Debug.Log($"Lever {gameObject.name} pulled. Calling CheckAndUnlockDoor.");
            if (basementDoorController != null) // Check if the reference is valid
            {
                basementDoorController.CheckAndUnlockDoor(); // Call the check
            }
            else
            {
                Debug.LogWarning($"BasementDoorController is not assigned to the Lever on {gameObject.name}!");
            }
        }
    }
    //if (leverController != null && !isPulled && inReach && Input.GetKeyDown(pullKey))
    //{
    //    isPulled = true;
    //    leverController.PlayAnimation();
    //}
    //else if (leverController != null && isPulled && inReach && Input.GetKeyDown(pullKey))
    //{
    //    isPulled = false;
    //    leverController.PlayAnimation();
    //}

}

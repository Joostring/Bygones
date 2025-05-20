// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransDoor : MonoBehaviour
{
   
    [SerializeField]private Crossfade crossfade;
    [SerializeField]private InspectSystem inspectSystem;
    
    [SerializeField] private bool isOpen = false;
    private bool atDoor = false;
   

    [Header("Put the name of the key here")]
    [SerializeField] private string nameOfKeyForDoor;
   
    [Header("Audio")]
    [Tooltip("The audiosource for opening the door")]
    [SerializeField] private AudioSource Open = null;
    [SerializeField] private float openDelay = 0f;
   


    private void Update()
    {
        if (inspectSystem.HasItem("Key_Basement_Inspect"))
        {
            isOpen = true;
        }
        SceneTransition();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            atDoor = true;
        }
    }
    public void SceneTransition()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.E) && atDoor)
        {
            Open.PlayDelayed(openDelay);
            crossfade.LoadNextScene();
        }



    }

    public string GetRequiredKey()
    {
        return nameOfKeyForDoor;
    }

}

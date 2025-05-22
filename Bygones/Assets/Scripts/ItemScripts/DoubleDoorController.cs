// Author Jonas Östring, Ylva Sundblad

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorController : MonoBehaviour
{
    private Animator doorAnim;
    //private bool doorOpen = false;
    private bool d_doorOpen = false;
    

    [Header("Put the name of the key here")]
    [SerializeField] private string nameOfKeyForDoor;

    [Header("Animation Names")]    
    [SerializeField] private string doubleOpenAnimationName = "D_DoorOpen";
    [SerializeField] private string doubleCloseAnimationName = "D_DoorClose";

    [Header("Pause Timer")]
    [SerializeField] private int waitTimer = 1;
    [SerializeField] private bool pauseInteraction = false;


    [Header("Audio")]
    [Tooltip("The audiosource for opening the door")]
    [SerializeField] private AudioSource DoubleOpen = null;
    [SerializeField] private float openDelay = 0f;
    [Space(10)]
    [Tooltip("The audiosource for closing the door")]
    [SerializeField] private AudioSource DoubleClose = null;
    [SerializeField] private float closeDelay = 0f;

    private void Awake()
    {
        doorAnim = GetComponent<Animator>();
    }
   
    private IEnumerator PauseDoorInteraction()
    {
        pauseInteraction = true;
        yield return new WaitForSeconds(waitTimer);
        pauseInteraction = false;
    }

    

    public void PlayAnimationDouble()
    {
        if (!d_doorOpen && !pauseInteraction)
        {
            doorAnim.Play(doubleOpenAnimationName, 0, 0.0f);
            d_doorOpen = true;
            StartCoroutine(PauseDoorInteraction());
            DoubleOpen.PlayDelayed(openDelay);
            
        }
        else if (d_doorOpen && !pauseInteraction)
        {
            doorAnim.Play(doubleCloseAnimationName, 0, 0.0f);
            d_doorOpen = false;
            StartCoroutine(PauseDoorInteraction());
            DoubleClose.PlayDelayed(closeDelay);
        }
    }


    public string GetRequiredKey()
    {
        return nameOfKeyForDoor;
    }

}

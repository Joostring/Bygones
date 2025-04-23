// Author Jonas Östring, Ylva Sundblad

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDoorController : MonoBehaviour
{
    private Animator doorAnim;
    public bool doorOpen = false;

    [Header("Put the name of the key here")]
    [SerializeField] private string nameOfKeyForDoor;


    [Header("Animation Names")]
    [SerializeField] private string openAnimationName = "DoorOpen";
    [SerializeField] private string closeAnimationName = "DoorClose";
    [SerializeField] private string slamAnimationName = "SlamClose";
    

    [Header("Pause Timer")]
    [SerializeField] private int waitTimer = 1;
    [SerializeField] private bool pauseInteraction = false;


    [Header("Audio")]
    [Tooltip("The audiosource for opening the door")]
    [SerializeField] private AudioSource Open = null;    
    [SerializeField] private float openDelay = 0f;
    [Space(10)]
    [Tooltip("The audiosource for closing the door")]
    [SerializeField] private AudioSource Close = null;
    [SerializeField] private float closeDelay = 0f;
    [Space(10)]
    [Tooltip("The audiosource for slamming the door")]
    [SerializeField] private AudioSource SlamClose = null;
    [SerializeField] private float slamDelay = 0f;

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

    public void PlayAnimationSingle()
    {
        if (!doorOpen && !pauseInteraction)
        {
            doorAnim.Play(openAnimationName, 0, 0.0f);
            doorOpen = true;
            StartCoroutine(PauseDoorInteraction());
            Open.PlayDelayed(openDelay);
        }
        else if (doorOpen && !pauseInteraction)
        {
            doorAnim.Play(closeAnimationName, 0, 0.0f);
            doorOpen = false;
            StartCoroutine(PauseDoorInteraction());
            Close.PlayDelayed(closeDelay);
        }


    }

   public void SlamAnimation()
    {
        if (doorOpen) 
        {
            doorAnim.Play(slamAnimationName, 0, 0.0f);
            SlamClose.PlayDelayed(slamDelay); 
            doorOpen = false ;
        }
        
    }

    public string GetRequiredKey()
    {
        return nameOfKeyForDoor;
    }




}

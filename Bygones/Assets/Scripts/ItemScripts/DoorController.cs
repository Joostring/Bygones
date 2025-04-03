// Author Jonas Östring, Ylva Sundblad

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDoorController : MonoBehaviour
{
    private Animator doorAnim;
    private bool doorOpen = false;
    private bool d_doorOpen = false;

    [Header("Animation Names")]
    [SerializeField] private string openAnimationName = "DoorOpen";
    [SerializeField] private string closeAnimationName = "DoorClose";
    [SerializeField] private string doubleOpenAnimationName = "D_DoorOpen";
    [SerializeField] private string doubleCloseAnimationName = "D_DoorClose";

    [Header("Pause Timer")]
    [SerializeField] private int waitTimer = 1;
    [SerializeField] private bool pauseInteraction = false;


    [Header("Audio")]
    [Tooltip("The audiosource for opening the door")]
    [SerializeField] private AudioSource Open = null;
    [SerializeField] private AudioSource DoubleOpen = null;
    [SerializeField] private float openDelay = 0f;
    [Space(10)]
    [Tooltip("The audiosource for closing the door")]
    [SerializeField] private AudioSource Close = null;
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
   
}

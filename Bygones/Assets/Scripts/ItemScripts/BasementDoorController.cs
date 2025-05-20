// Author : Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementDoorController : MonoBehaviour
{
    private Animator doorAnim;
    public bool doorOpen = false;
    public bool leverIsActive = false;


    [SerializeField] public Lever lever1;
    [SerializeField] public Lever lever2;
    [SerializeField] public Lever lever3;
    [SerializeField] public Lever lever4;
    [SerializeField] public GameObject basementDoor;
    




    [Header("Animation Names")]
    [SerializeField] private string openAnimationName = "DoorOpen";
    [SerializeField] private string closeAnimationName = "DoorClose";


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
    [Tooltip("The audiosource for unlocking the door")]
    [SerializeField] public AudioSource unLock = null;
    [SerializeField] public float unLockDelay = 0f;



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

    public void PlayAnimation()
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

    public void CheckAndUnlockDoor()
    {
        Debug.Log("CheckAndUnlockDoor() called.");
        
        if (lever1 != null && lever1.isPulled &&
        lever2 != null && lever2.isPulled &&
        lever3 != null && !lever3.isPulled &&
        lever4 != null && lever4.isPulled)
        {
            
            basementDoor.tag = "Open";
            Debug.Log("All levers are right. Attempting to play unlock sound.");
            if (unLock != null)
            {
                Debug.Log("Unlock AudioSource is not null.");
                unLock.PlayDelayed(unLockDelay);
            }
            else
            {
                Debug.Log("Unlock AudioSource is NULL!");
            }
        }
    }

    public bool UnlockDoor()
    {

        return (lever1 != null && lever1.isPulled &&
        lever2 != null && lever2.isPulled &&
        lever3 != null && !lever3.isPulled &&
        lever4 != null && lever4.isPulled);
    }


    public void EnableLeverInteraction()
    {
        leverIsActive = true;
        Debug.Log("Painting interaction enabled.");
        // Optionally, you could also enable the Painting scripts here if they were initially disabled.
    }

}

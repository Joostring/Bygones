// Author : Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoorController : MonoBehaviour
{
    private Animator doorAnim;
    public bool doorOpen = false;
    public bool paintingIsActive = false;

    [SerializeField] public Painting treePainting;
    [SerializeField] public Painting peachPainting;
    [SerializeField] public GameObject puzzleDoor;
    

    


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
        Debug.Log($"Checking unlock: Tree Turned: {treePainting?.isTurned}, Peach Turned: {peachPainting?.isTurned}");
        if (treePainting != null && peachPainting != null && treePainting.isTurned && peachPainting.isTurned)
        {
            puzzleDoor.tag = "Open";
            Debug.Log("Both paintings are turned. Attempting to play unlock sound.");
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
        Debug.Log($"UnlockDoor called: Tree Ref: {treePainting}, Peach Ref: {peachPainting}");
        Debug.Log($"UnlockDoor called: Tree Turned: {treePainting?.isTurned}, Peach Turned: {peachPainting?.isTurned}");
        return (treePainting != null && peachPainting != null && treePainting.isTurned && peachPainting.isTurned);
    }


    public void EnablePaintingInteraction()
    {
        paintingIsActive = true;
        Debug.Log("Painting interaction enabled.");
        // Optionally, you could also enable the Painting scripts here if they were initially disabled.
    }











}

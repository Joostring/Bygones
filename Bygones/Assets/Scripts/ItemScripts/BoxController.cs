// Author : Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Animator boxAnim;
    public bool boxOpen = false;

    [Header("Put the name of the key here")]
    [SerializeField] private string nameOfKeyForLock;


    [Header("Animation Names")]
    [SerializeField] private string openAnimationName = "Open";
    [SerializeField] private string closeAnimationName = "Close";
    


    [Header("Pause Timer")]
    [SerializeField] private int waitTimer = 1;
    [SerializeField] private bool pauseInteraction = false;


    [Header("Audio")]
    [Tooltip("The audiosource for opening the box")]
    [SerializeField] private AudioSource Open = null;
    [SerializeField] private float openDelay = 0f;
    [Space(10)]
    [Tooltip("The audiosource for closing the box")]
    [SerializeField] private AudioSource Close = null;
    [SerializeField] private float closeDelay = 0f;
    

    private void Awake()
    {
        boxAnim = GetComponent<Animator>();
    }

    private IEnumerator PauseBoxInteraction()
    {
        pauseInteraction = true;
        yield return new WaitForSeconds(waitTimer);
        pauseInteraction = false;
    }

    public void PlayAnimation()
    {
        if (!boxOpen && !pauseInteraction)
        {
            boxAnim.Play(openAnimationName, 0, 0.0f);
            boxOpen = true;
            StartCoroutine(PauseBoxInteraction());
            Open.PlayDelayed(openDelay);
        }
        else if (boxOpen && !pauseInteraction)
        {
            boxAnim.Play(closeAnimationName, 0, 0.0f);
            boxOpen = false;
            StartCoroutine(PauseBoxInteraction());
            Close.PlayDelayed(closeDelay);
        }


    }

   

    public string GetRequiredKey()
    {
        return nameOfKeyForLock;
    }

}

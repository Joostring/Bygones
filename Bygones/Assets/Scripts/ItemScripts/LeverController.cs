// Author : Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    private Animator leverAnim;
    private bool isPulled;

    [Header("Animation Names")]
    [SerializeField] private string upAnim;
    [SerializeField] private string downAnim;

    [Header("Pause Timer")]
    [SerializeField] private int waitTimer = 1;
    [SerializeField] private bool pauseInteraction = false;


    [Header("Audio")]
    [Tooltip("The audiosource for pulling the lever")]
    [SerializeField] private AudioSource up = null;
    [SerializeField] private float upDelay = 0f;
    [SerializeField] private AudioSource down = null;
    [SerializeField] private float downDelay = 0f;

    private void Awake()
    {
        leverAnim = GetComponent<Animator>();
    }

    private IEnumerator PauseLeverInteraction()
    {
        pauseInteraction = true;
        yield return new WaitForSeconds(waitTimer);
        pauseInteraction = false;
    }



    public void PlayAnimation()
    {
        if (!isPulled && !pauseInteraction)
        {
            leverAnim.Play(downAnim, 0, 0.0f);
            isPulled = true;
            StartCoroutine(PauseLeverInteraction());
            down.PlayDelayed(downDelay);
        }
        else if (isPulled && !pauseInteraction)
        {
            leverAnim.Play(upAnim, 0, 0.0f);
            isPulled = false;
            StartCoroutine(PauseLeverInteraction());
            up.PlayDelayed(upDelay);
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDoorController : MonoBehaviour
{

    private Animator doorAnimator;

    private bool doorOpen;
    [SerializeField] string openClip;
    [SerializeField] string closeClip;
    private void Awake()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation() 
    {
        if (!doorOpen) 
        {
            doorAnimator.Play(openClip, 0, 0.0f);
            doorOpen = true;
        }
        else 
        {
            doorAnimator.Play(closeClip, 0, 0.0f);
            doorOpen = false;
        }
    
    }
}

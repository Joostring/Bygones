using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDoorController : MonoBehaviour
{

    private Animator doorAnimator;

    private bool doorOpen;

    private void Awake()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation() 
    {
        if (!doorOpen) 
        {
            doorAnimator.Play("DoorOpen", 0, 0.0f);
            doorOpen = true;
        }
        else 
        {
            doorAnimator.Play("DoorClose", 0, 0.0f);
            doorOpen = false;
        }
    
    }
}

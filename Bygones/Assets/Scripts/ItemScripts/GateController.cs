using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private Animator gateAnim;
    //private bool doorOpen = false;
    private bool gateOpen = false;

    [SerializeField] private InspectSystem inspectSystem;

    [Header("Put the name of the key here")]
    [SerializeField] private string nameOfKeyForGate;

    [Header("Animation Names")]
    [SerializeField] private string OpenAnimationName = "IronGateOpen";
    //[SerializeField] private string CloseAnimationName = "D_DoorClose";

    [Header("Pause Timer")]
    [SerializeField] private int waitTimer = 1;
    [SerializeField] private bool pauseInteraction = false;


    [Header("Audio")]
    [Tooltip("The audiosource for opening the gate")]
    [SerializeField] private AudioSource GateOpen = null;
    [SerializeField] private float openDelay = 0f;
    //[Space(10)]
    //[Tooltip("The audiosource for closing the door")]
    //[SerializeField] private AudioSource DoubleClose = null;
    //[SerializeField] private float closeDelay = 0f;

    private void Awake()
    {
        gateAnim = GetComponent<Animator>();
    }

    private IEnumerator PauseDoorInteraction()
    {
        pauseInteraction = true;
        yield return new WaitForSeconds(waitTimer);
        pauseInteraction = false;
    }



    public void PlayAnimation()
    {
        if (!gateOpen && !pauseInteraction && inspectSystem.HasItem("Key_Gate_Inspect"))
        {
            gateAnim.Play(OpenAnimationName, 0, 0.0f);
            gateOpen = true;
            StartCoroutine(PauseDoorInteraction());
            GateOpen.PlayDelayed(openDelay);
        }
        //else if (d_doorOpen && !pauseInteraction)
        //{
        //    doorAnim.Play(doubleCloseAnimationName, 0, 0.0f);
        //    d_doorOpen = false;
        //    StartCoroutine(PauseDoorInteraction());
        //    DoubleClose.PlayDelayed(closeDelay);
        //}
    }


    public string GetRequiredKey()
    {
        return nameOfKeyForGate;
    }

}

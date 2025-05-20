// Author : Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingController : MonoBehaviour
{
    private Animator paintingAnim;
    public bool isTurned = false;

    [Header("Animation Names")]
    [SerializeField] private string animName;

    [Header("Pause Timer")]
    [SerializeField] private int waitTimer = 1;
    [SerializeField] private bool pauseInteraction = false;

    [Header("Audio")]
    [Tooltip("The audiosource for turning the painting")]
    [SerializeField] private AudioSource Turn = null;
    [SerializeField] private float turnDelay = 0f;

    private void Awake()
    {
        paintingAnim = GetComponent<Animator>();
    }

    private IEnumerator PausePaintingInteraction()
    {
        pauseInteraction = true;
        yield return new WaitForSeconds(waitTimer);
        pauseInteraction = false;
    }

    public void PlayAnimation()
    {
        if (!isTurned && !pauseInteraction)
        {
            paintingAnim.Play(animName, 0, 0.0f);
            isTurned = true;
            StartCoroutine(PausePaintingInteraction());
            Turn.PlayDelayed(turnDelay);
        }
    }
}

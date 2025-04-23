using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LowSanityTimer : MonoBehaviour
{
    [SerializeField] public float sanityProcentage = 100f;
    [SerializeField] PostProcessVolume sanityVolume;
    [SerializeField] float sanityDrainRate = 5f;

    bool isSanityDraing = true;
    bool hasTriggedFade = false;
    [SerializeField] Fadein[] itemsToFadeIn;
    [SerializeField] PlayerMovement playermovement;

   //[SerializeField] private SanitySounds sanitySounds;

    //private void Awake()
    //{
    //    sanitySounds = GetComponent<SanitySounds>();
    //}

    // Update is called once per frame
    void Update()
    {
        if (isSanityDraing)
        {
            SanityDraining();
        }
        //sanitySounds.PlaySanity();
    }

    

    private void SanityDraining()
    {
        sanityProcentage -= sanityDrainRate * Time.deltaTime;
        sanityProcentage = Mathf.Clamp(sanityProcentage, 0, 100);

        float weight = Mathf.InverseLerp(100, 0, sanityProcentage);
        sanityVolume.weight = weight;


        if (sanityProcentage <= 50f && !hasTriggedFade && itemsToFadeIn.Length > 0)
        {
            foreach(Fadein item in itemsToFadeIn)
            {
                if(item != null)
                {
                    item.StartFadeIn();
                }
                //else if(item != null && !isSanityDraing)
                //{
                //    item.StopFadeIn();
                //}
            }
            hasTriggedFade = true;
           
        }

        if (sanityProcentage <= 25f)
        {
            playermovement.SetReversedMovementState(true);
            playermovement.SetMovementState(false);
            
        }
        else
        {
            playermovement.SetReversedMovementState(false);
            playermovement.SetMovementState(true);
        }
    }
    public void SanityDrainChecker(bool state)
    {
        isSanityDraing = state;
    }
    public void SanityGain(float value)
    {
        float previousSanity = sanityProcentage;
        sanityProcentage += value;
        if (previousSanity <= 25f && sanityProcentage > 25f && itemsToFadeIn.Length > 0)
        {
            foreach (Fadein item in itemsToFadeIn)
            {
                if (item != null)
                {
                    item.ResetFade();
                }
            }
            hasTriggedFade = false; 
        }
    }
    public void SanityLoss(float value)
    {
        sanityProcentage -= value;
    }
}

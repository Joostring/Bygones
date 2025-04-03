using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LowSanityTimer : MonoBehaviour
{
    [SerializeField] float sanityProcentage = 100f;
    [SerializeField] PostProcessVolume sanityVolume;
    [SerializeField] float sanityDrainRate = 5f;

    bool isSanityDraing = true;
    bool hasTriggedFade = false;
    [SerializeField] Fadein[] itemsToFadeIn;
    [SerializeField] PlayerMovement playermovement;



    // Update is called once per frame
    void Update()
    {
        if (isSanityDraing)
        {
            SanityDraining();
        }
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

        if(sanityProcentage <= 50f)
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
        if (previousSanity <= 50f && sanityProcentage > 50f && itemsToFadeIn.Length > 0)
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

}

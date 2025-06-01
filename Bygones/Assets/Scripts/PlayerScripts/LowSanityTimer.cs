// Author Mikael

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LowSanityTimer : MonoBehaviour
{
    [SerializeField] public float sanityProcentage = 100f;
    [SerializeField] public PostProcessVolume sanityVolume;
    [SerializeField] float sanityDrainRate = 5f;

    public bool isSanityDraing;
    bool hasTriggedFade = false;
    
    [SerializeField] PlayerMovement playermovement;

    

    private void Start()
    {
        isSanityDraing = false;
        
        sanityVolume.weight = 0;
    }

    
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
        sanityVolume.weight = Mathf.InverseLerp(100, 0, sanityProcentage);
        
    }
    public void SanityDrainChecker(bool state)
    {
        isSanityDraing = state;
    }
    public void SanityGain(float value)
    {
        float previousSanity = sanityProcentage;
        sanityProcentage += value;
        
    }
    public void SanityLoss(float value)
    {
        sanityProcentage -= value;
    }

    public float GetSanity()
    {
        return sanityProcentage;
    }
}

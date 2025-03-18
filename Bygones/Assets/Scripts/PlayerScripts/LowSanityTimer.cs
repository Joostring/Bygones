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

    private bool isSanityDraing = true;


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
    }
    public void SanityDrainChecker(bool state)
    {
        isSanityDraing = state;
    }
    public void PillSanityGain(float value)
    {
        sanityProcentage += value;
    }
}

// Author : Jonas �string

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    [SerializeField] private LowSanityTimer sanity;
    [SerializeField] private float sanityGainRate = 1.0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sanity.isSanityDraing = false;

            if (!sanity.isSanityDraing)
            {
                sanity.sanityProcentage += sanityGainRate * Time.deltaTime;
                sanity.sanityProcentage = Mathf.Clamp(sanity.sanityProcentage, 0, 100);

                float weight = Mathf.InverseLerp(100, 0, sanity.sanityProcentage);
                sanity.sanityVolume.weight = weight;

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sanity.isSanityDraing = true;
        }
    }
}

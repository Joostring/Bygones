// Author : Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlickeringLight : MonoBehaviour
{
    private Light lightToFlicker;
    [SerializeField, Range(0f, 10f)] private float minIntensity = 0.5f;
    [SerializeField, Range(0f, 10f)] private float maxIntensity = 1.5f;
    [SerializeField,Min(0f)] private float timeBetweenIntensity = 0.1f;

    private float currentTimer;

    private void Awake()
    {
        if (lightToFlicker == null)
        {
            lightToFlicker = GetComponent<Light>();
        }

        ValidateIntensityBounds();
    }

    private void Update()
    {
        currentTimer += Time.deltaTime;
        if ( !(currentTimer>=timeBetweenIntensity))
        {
            return;
        }
        lightToFlicker.intensity = Random.Range(minIntensity, maxIntensity);
        currentTimer = 0f;
    }

    private void ValidateIntensityBounds() // Makes sure the values of intensity stays right
    {
        if (!(minIntensity > maxIntensity))
        {
            return;
        }
        Debug.LogWarning("Min intensity is greater than max intensity, swapping values");
        (minIntensity,maxIntensity) = (maxIntensity,minIntensity);
    }

}

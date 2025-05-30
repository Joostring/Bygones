using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.15f;
    public float shakeMagnitude = 0.1f;
    [SerializeField] private AudioSource screamSound;

    private Vector3 originalPosition;
    private float currentShakeTime = 0f;

    public void TriggerShake()
    {
        originalPosition = transform.localPosition;
        currentShakeTime = shakeDuration;
        if (screamSound != null)
        {
            screamSound.Play();
        }
    }

    void Update()
    {
        if (currentShakeTime > 0)
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            currentShakeTime -= Time.deltaTime;

            if (currentShakeTime <= 0)
            {
                transform.localPosition = originalPosition; // Återställ positionen
            }
        }
    }
}
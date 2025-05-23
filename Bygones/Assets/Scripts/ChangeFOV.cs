using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFOV : MonoBehaviour
{
    [SerializeField] private float newFOV = 30f;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private Camera cam;
    
    private float originalFOV;
    private bool isShrinking;
    void Start()
    {
        originalFOV = cam.fieldOfView;
    }


    void Update()
    {
        {
            if (isShrinking)
            {
                cam.fieldOfView = Mathf.Lerp(newFOV, originalFOV, Time.deltaTime / transitionTime);
                if (cam.fieldOfView >= originalFOV)
                {
                    RestoreFOV();
                }
            }
            else
            {
               cam.fieldOfView = Mathf.Lerp(originalFOV, newFOV, Time.deltaTime / transitionTime);
            }
        } 
    }

    public void ShrinkFOV()
    {
        isShrinking = true;
    }

    public void RestoreFOV()
    {
        isShrinking = false;
    }
} 
    


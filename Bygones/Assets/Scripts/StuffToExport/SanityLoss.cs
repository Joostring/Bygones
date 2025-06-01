//Author Mikael
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityLoss : MonoBehaviour
{
    [SerializeField] LowSanityTimer lowSanityTimer;
    [SerializeField] private GameObject cameraShakerObject;
    public Transform InteractorSource;
    public float InteractRange = 3f;
    public LayerMask interactableLayer;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, InteractRange, interactableLayer))
            {

                ObjectSanityLoss sanityLossAmount = hitInfo.collider.GetComponent<ObjectSanityLoss>();
                if (sanityLossAmount != null && !sanityLossAmount.hasTriggerdSanityLoss)
                {
                    lowSanityTimer.SanityLoss(sanityLossAmount.amountSanityLoss);
                    sanityLossAmount.hasTriggerdSanityLoss = true;
                    CameraShake cameraShake = cameraShakerObject.GetComponent<CameraShake>();
                    if (cameraShakerObject != null)
                    {

                        if (cameraShake != null)
                        {
                            cameraShake.TriggerShake();
                        }
                    }

                }
            }


        }
    }
}


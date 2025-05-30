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
    // Update is called once per frame
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

////Author Mikael
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SanityLoss : MonoBehaviour
//{
//    [SerializeField] LowSanityTimer lowSanityTimer;
//    [SerializeField] private GameObject cameraShakerObject;
//    public Transform InteractorSource;
//    public float InteractRange = 3f;
//    public LayerMask interactableLayer;

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
//            RaycastHit hitInfo;

//            if (Physics.Raycast(ray, out hitInfo, InteractRange, interactableLayer))
//            {
//                ObjectSanityLoss sanityLossAmount = hitInfo.collider.GetComponent<ObjectSanityLoss>();
//                if (sanityLossAmount != null)
//                {
//                    Debug.Log("Checking hasTriggerdSanityLoss for: " + hitInfo.collider.gameObject.name + " - " + sanityLossAmount.hasTriggerdSanityLoss);
//                    if (!sanityLossAmount.hasTriggerdSanityLoss)
//                    {
//                        Debug.Log("Applying sanity loss and triggering shake.");
//                        lowSanityTimer.SanityLoss(sanityLossAmount.amountSanityLoss);
//                        sanityLossAmount.hasTriggerdSanityLoss = true;

//                        if (cameraShakerObject != null)
//                        {
//                            CameraShake cameraShake = cameraShakerObject.GetComponent<CameraShake>();
//                            if (cameraShake != null)
//                            {
//                                cameraShake.TriggerShake();
//                            }
//                        }
//                    }
//                    else if (sanityLossAmount != null && sanityLossAmount.hasTriggerdSanityLoss)
//                    {
//                        Debug.Log("Sanity loss already triggered for: " + hitInfo.collider.gameObject.name);
//                        // We should NOT trigger the camera shake here
//                    }
//                    else
//                    {
//                        Debug.Log("No ObjectSanityLoss component found on hit object: " + hitInfo.collider.gameObject.name);
//                    }
//                }
//                else
//                {
//                    Debug.Log("No ObjectSanityLoss component on hit object.");
//                }
//            }
//            else
//            {
//                //Debug.Log("No interactable object in range.");
//            }
//        }
//    }
//}
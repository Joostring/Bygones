//Author Mikael
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityLoss : MonoBehaviour
{
    [SerializeField] LowSanityTimer lowSanityTimer;
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

                }
            }
        }
    }
}

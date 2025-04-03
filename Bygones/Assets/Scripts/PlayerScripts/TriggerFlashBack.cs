using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFlashBack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] FlashBackEvent flashBackEvent;
    [SerializeField] PlayerMovement playerMovement;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && flashBackEvent != null)
        {
            flashBackEvent.SetFlashBackEvent(true);
            playerMovement.SetStopMovement(true);
            //playerMovement.SetMovementState(false);
            //playerMovement.SetReversedMovementState(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && flashBackEvent != null)
        {
            flashBackEvent.SetFlashBackEvent(false);
            playerMovement.SetStopMovement(false);
            //playerMovement.SetMovementState(true);
            //playerMovement.SetReversedMovementState(true);
        }
    }
}

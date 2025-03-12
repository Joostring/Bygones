using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFlashBack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] FlashBackEvent flashBackEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && flashBackEvent != null)
        {
            flashBackEvent.SetFlashBackEvent(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && flashBackEvent != null)
        {
            flashBackEvent.SetFlashBackEvent(false);
        }
    }
}

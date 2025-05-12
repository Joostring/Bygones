using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerFlashBack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] FlashBackEvent flashBackEvent;
    [TextArea(10, 10)][SerializeField] public List<string> flashbackTexts = new List<string>();
    [SerializeField] public float textDisplayDuration = 0f;
    public bool hasTriggerdFlashback = false;
 
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && flashBackEvent != null && !hasTriggerdFlashback)
        {
            flashBackEvent.StartFlashback(this);
        }
    }
}

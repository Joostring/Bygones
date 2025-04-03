using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FlashBackEvent : MonoBehaviour
{
    [SerializeField] PostProcessLayer postProcessLayer;
    [SerializeField] LowSanityTimer lowSanityTimer;
    [SerializeField] LayerMask defaultLayer;
    [SerializeField] LayerMask greyLayer;
    [SerializeField] GameObject cubeTrigger;
    private bool isFlashBack = false;
    [SerializeField] float moveTrigger = 0f;
    [SerializeField] float moveDelay = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlashBack)
        {
            isFlashBack = true;
            postProcessLayer.volumeLayer = greyLayer;
            lowSanityTimer.SanityDrainChecker(false);
            moveTrigger -= moveDelay * Time.deltaTime;
            if(moveTrigger <= 0)
            {
                cubeTrigger.transform.position = new Vector3(0, 0, 0);
            }
        }
        else
        {
            isFlashBack = false;
            postProcessLayer.volumeLayer = defaultLayer;
            lowSanityTimer.SanityDrainChecker(true);

        }
    }
 

    public void SetFlashBackEvent(bool state)
    {
        isFlashBack = state;
    }

 


}

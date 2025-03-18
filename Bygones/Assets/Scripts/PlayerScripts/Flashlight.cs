// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    [Tooltip("The light of the flashlight")]
    [SerializeField] GameObject LightSource;
   
    private bool flashLightEnabled = false;
    void Start()
    {
        LightSource.gameObject.SetActive(false);
        
    }

    
    void Update()
    {

        //FlashLightON();

        if (Input.GetKey(KeyCode.F))
        {
            if (!flashLightEnabled)
            {
                LightSource.gameObject.SetActive(true);
                flashLightEnabled = true;
            }
            else
            {
                LightSource.gameObject.SetActive(false);
                flashLightEnabled = false;
            }
        }
    }

    private void FlashLightON()
    {
        //if (Input.GetKey(KeyCode.F))
        //{
        //    if (!flashLightEnabled)
        //    {
        //        LightSource.gameObject.SetActive(true);
        //        flashLightEnabled = true;
        //    }
        //    else
        //    {
        //        LightSource.gameObject.SetActive(false);
        //        flashLightEnabled = false;
        //    }
        //}
    }

    public void FlashlightON(InputValue input)
    {
       
        //if (!flashLightEnabled)
        //{
        //    LightSource.gameObject.SetActive(true);
        //    flashLightEnabled = true;
        //}
        //else
        //{
        //    LightSource.gameObject.SetActive(false);
        //    flashLightEnabled = false;
        //}
    }

    public void FlashlightOFF(InputValue input)
    {
        //LightSource.gameObject.SetActive(false);
        //flashLightEnabled = false;
    }
}

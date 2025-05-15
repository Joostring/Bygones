// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Flashlight : MonoBehaviour
{
    [Tooltip("The light of the flashlight")]
    [SerializeField] GameObject LightSource;
    private bool flashTimer = true;

    [SerializeField] private InspectSystem inspectSystem;
   
    private bool flashLightEnabled = false;
    void Start()
    {
        LightSource.gameObject.SetActive(false);
        
    }

    
    void Update()
    {

        if (Input.GetKey(KeyCode.F) && flashTimer == true && inspectSystem.HasItem("Flashlight_Inspect"))
        {
            StartCoroutine(FlashlightCoooldown());
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

    public IEnumerator FlashlightCoooldown()
    {
        flashTimer = false;
        yield return new WaitForSeconds(2);
        flashTimer = true;
        

    }




}

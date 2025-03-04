using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    // Start is called before the first frame update
    Camera camera;
    [SerializeField] private int interactionRange = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;

    private MyDoorController raycastedObject;

    [SerializeField] private KeyCode openDoorKey = KeyCode.E;
    [SerializeField] private Image crosshair = null;

    private bool isCrosshairActive;
    private bool doOnce;

    private const string interactableTag = "InteractiveObject";

    void Start()
    {
        camera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, interactionRange, mask)) 
        {
            if (hit.collider.CompareTag(interactableTag)) 
            {
                if (!doOnce) 
                {

                    raycastedObject = hit.collider.gameObject.GetComponent<MyDoorController>();
                    CrosshairChange(true);
                }
                isCrosshairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(openDoorKey)) 
                {

                    raycastedObject.PlayAnimation();
                }
            }

        }
        else
        {
            if (isCrosshairActive) 
            {
                CrosshairChange(false);
                doOnce = false;
            
            }

        }
    }

    void CrosshairChange(bool on) 
    { 
        if (on && !doOnce) 
        {

            crosshair.color = Color.red;

        }
        else 
        {

            crosshair.color = Color.white;
            isCrosshairActive = false;
        }
    
    }

    private void OnInteract() 
    { 
    
    }
}

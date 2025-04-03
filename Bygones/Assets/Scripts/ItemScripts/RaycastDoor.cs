// Author Ylva Sundblad

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastDoor : MonoBehaviour
{
    [SerializeField] private int rayLenght = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;


    private BasicDoorController raycastedObject;

    [SerializeField] private KeyCode openDoorKey = KeyCode.E;
    [SerializeField] private Image crosshair = null;
    private bool isCrosshairActive;
    private bool doOnce;

    private const string interactebleTag = "Open";

    private void Update()
    {
        RaycastHit hit;
        //Vector3 forward = transform.TransformDirection(Vector3.forward);

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position + new Vector3(0,1,0), forward, Color.green);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if(Physics.Raycast(transform.position + new Vector3(0,1,0),forward,out hit, rayLenght, mask))
        {
            if(hit.collider.CompareTag(interactebleTag))
            {
                if(!doOnce)
                {
                    raycastedObject = hit.collider.gameObject.GetComponent<BasicDoorController>();
                    CrosshairChange(true);
                }

                isCrosshairActive = true;
                doOnce = true;

                //if(Input.GetKeyDown(openDoorKey))
                //{
                //    raycastedObject.PlayAnimationSingle();
                //}
                if (Input.GetKeyDown(openDoorKey))
                {
                    raycastedObject.PlayAnimationDouble();
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

    private void CrosshairChange(bool on)
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
}

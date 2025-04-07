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
    private InspectSystem inspectsystem;
    private string KeyNeededForDoor;

    private SingleDoorController singleDoorRay;
    private DoubleDoorController doubleDoorRay;

    [SerializeField] private KeyCode openDoorKey = KeyCode.E;
    [SerializeField] private Image crosshair = null;
    private bool isCrosshairActive;
    private bool doOnce;

    private const string openTag = "Open";
    private const string lockedTag = "Locked";

    private void Start()
    {
        inspectsystem = FindObjectOfType<InspectSystem>();
        singleDoorRay = FindObjectOfType<SingleDoorController>();
        doubleDoorRay = FindObjectOfType<DoubleDoorController>();
    }

    //private void Update()
    //{
    //    RaycastHit hit;
    //    //Vector3 forward = transform.TransformDirection(Vector3.forward);

    //    Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
    //    //Debug.DrawRay(transform.position + new Vector3(0,1,0), forward, Color.green);

    //    int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

    //    if(Physics.Raycast(transform.position + new Vector3(0,1,0),forward,out hit, rayLenght, mask))
    //    {
    //        if(hit.collider.CompareTag(interactebleTag))
    //        {
    //            if(!doOnce)
    //            {
    //                singleDoorRay = hit.collider.gameObject.GetComponent<SingleDoorController>();
    //                doubleDoorRay = hit.collider.gameObject.GetComponent<DoubleDoorController>();
    //                //CrosshairChange(true);
    //            }

    //            isCrosshairActive = true;
    //            doOnce = true;


    //            if (Input.GetKeyDown(openDoorKey))
    //            {
    //                if(singleDoorRay != null)
    //                {
    //                    singleDoorRay.PlayAnimationSingle();
    //                }
    //                else if (doubleDoorRay != null)
    //                {
    //                    doubleDoorRay.PlayAnimationDouble();
    //                }
                    
    //            }
    //            //if (Input.GetKeyDown(openDoorKey))
    //            //{
    //            //    doubleDoorRay.PlayAnimationDouble();
    //            //}
    //        }
    //    }
    //    else
    //    {
    //        if (isCrosshairActive)
    //        {
    //           // CrosshairChange(false);
    //            doOnce = false;
    //        }
    //    }
    //}


    private void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), forward, out hit, rayLenght, mask))
        {
            if (hit.collider.CompareTag(lockedTag))
            {
                DoubleDoorController D_currentDoor = hit.collider.GetComponent<DoubleDoorController>();
                SingleDoorController S_currentDoor = hit.collider.GetComponent<SingleDoorController>();

                if (D_currentDoor != null)
                {
                    string keyRequired = D_currentDoor.GetRequiredKey();

                    if (Input.GetKeyDown(openDoorKey))
                    {
                        if (inspectsystem.HasItem(keyRequired))
                        {
                            Debug.Log("Du lyckades öppnade dörren med nyckeln: " + keyRequired);
                            D_currentDoor.PlayAnimationDouble();
                        }
                        else
                        {
                            Debug.Log("Du behöver nyckeln: " + keyRequired);
                        }
                    }
                }

                if (S_currentDoor != null)
                {
                    string keyRequired = S_currentDoor.GetRequiredKey();

                    if (Input.GetKeyDown(openDoorKey))
                    {
                        if (inspectsystem.HasItem(keyRequired))
                        {
                            Debug.Log("Du lyckades öppnade dörren med nyckeln: " + keyRequired);
                            S_currentDoor.PlayAnimationSingle();
                        }
                        else
                        {
                            Debug.Log("Du behöver nyckeln: " + keyRequired);
                        }
                    }
                }
            }
            if (hit.collider.CompareTag(openTag))
            {
                DoubleDoorController D_currentDoor = hit.collider.GetComponent<DoubleDoorController>();
                SingleDoorController S_currentDoor = hit.collider.GetComponent<SingleDoorController>();

                if (Input.GetKeyDown(openDoorKey))
                {
                    if (S_currentDoor != null)
                    {
                        S_currentDoor.PlayAnimationSingle();
                    }
                    else if (D_currentDoor != null)
                    {
                        D_currentDoor.PlayAnimationDouble();
                    }
                }
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

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RaycastItem : MonoBehaviour
{
    
    [SerializeField] private int rayLength = 5;
    [SerializeField] private KeyCode pickUpKey = KeyCode.E;
    [SerializeField] private Image crosshair = null;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;
    //[SerializeField] private Item item;
    private bool isCrosshairActive;
    private bool doOnce;

    private ItemController raycastedObject;

    public Item item;

    private const string interactebleTag = "InteractiveObject";

   
    

    private void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

      

        if (Physics.Raycast(transform.position, forward, out hit, rayLength, mask))
        {
            if (hit.collider.CompareTag(interactebleTag))
            {
                if (!doOnce)
                {
                    raycastedObject = hit.collider.gameObject.GetComponent<ItemController>();
                    Pickup();
                    CrosshairChange(true);
                }

                isCrosshairActive = true;
                doOnce = true;

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

    void Pickup()
    {
        InventoryManager.Instance.Add(item);
        //Destroy(gameObject);
    }
}

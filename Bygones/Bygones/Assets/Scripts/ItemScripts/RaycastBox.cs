using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RaycastBox : MonoBehaviour
{

    [SerializeField] private int rayLenght = 1;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;
    private InspectSystem inspectsystem;
    private string KeyNeededForDoor;
    

    private BoxController boxController;

    [SerializeField] private KeyCode openDoorKey = KeyCode.E;
    [SerializeField] private Image crosshair = null;
    

    private const string openTag = "Open";
    private const string lockedTag = "Locked";

    private void Start()
    {
        inspectsystem = FindObjectOfType<InspectSystem>();
        boxController = FindObjectOfType<BoxController>();
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), forward, out hit, rayLenght, mask))
        {
            if (hit.collider.CompareTag(lockedTag))
            {
                BoxController boxController = hit.collider.GetComponent<BoxController>();
                
                if (boxController != null)
                {
                    string keyRequired = boxController.GetRequiredKey();

                    if (Input.GetKeyDown(openDoorKey))
                    {
                        if (inspectsystem.HasItem(keyRequired))
                        {
                            Debug.Log("Du lyckades öppnade dörren med nyckeln: " + keyRequired);
                            boxController.PlayAnimation();
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
                BoxController boxController = hit.collider.GetComponent<BoxController>();

                if (Input.GetKeyDown(openDoorKey))
                {
                    Debug.Log("Trying to open");
                    if (boxController != null)
                    {
                        boxController.PlayAnimation();
                    }
                }
            }
        }
    }
}

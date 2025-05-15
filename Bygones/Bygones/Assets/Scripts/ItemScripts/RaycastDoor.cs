// Author Ylva Sundblad, Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastDoor : MonoBehaviour
{
    [SerializeField] private int rayLenght = 2;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;
    private InspectSystem inspectsystem;
    private string KeyNeededForDoor;

    private SingleDoorController singleDoorRay;
    private DoubleDoorController doubleDoorRay;
    private GateController gateController;

    [SerializeField] private KeyCode openDoorKey = KeyCode.E;
    [SerializeField] private Image crosshair = null;
    private bool isCrosshairActive;
    private bool doOnce;

    private const string openTag = "Open";
    private const string lockedTag = "Locked";

    [SerializeField] private ProgressSystem progressSystem;

    private void Start()
    {
        inspectsystem = FindObjectOfType<InspectSystem>();
        singleDoorRay = FindObjectOfType<SingleDoorController>();
        doubleDoorRay = FindObjectOfType<DoubleDoorController>();
        gateController = FindObjectOfType<GateController>();
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
                DoubleDoorController D_currentDoor = hit.collider.GetComponent<DoubleDoorController>();
                SingleDoorController S_currentDoor = hit.collider.GetComponent<SingleDoorController>();
                GateController currentGate = hit.collider.GetComponent<GateController>();

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

                            ProgressNoteData noteData = D_currentDoor.GetComponentInParent<ProgressNoteData>();
                            if (noteData != null && !noteData.noteAlreadyAdded)
                            {
                                foreach (string line in noteData.noteLines)
                                {
                                    progressSystem.AddNote(line);
                                    noteData.noteAlreadyAdded = true;
                                }
                            }
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

                            ProgressNoteData noteData = S_currentDoor.GetComponentInParent<ProgressNoteData>();
                            if (noteData != null && !noteData.noteAlreadyAdded)
                            {
                                foreach (string line in noteData.noteLines)
                                {
                                    progressSystem.AddNote(line);
                                    noteData.noteAlreadyAdded = true;
                                }
                            }
                        }
                    }
                }

                if(currentGate != null)
                {
                    string keyRequired = currentGate.GetRequiredKey();

                    if (Input.GetKey(openDoorKey))
                    {
                        if (inspectsystem.HasItem(keyRequired))
                        {
                            Debug.Log("Du lyckades öppna grinden med nyckeln: " + keyRequired);
                            currentGate.PlayAnimation();
                        }
                    }
                }
            }
            if (hit.collider.CompareTag(openTag))
            {
                DoubleDoorController D_currentDoor = hit.collider.GetComponent<DoubleDoorController>();
                SingleDoorController S_currentDoor = hit.collider.GetComponent<SingleDoorController>();
                GateController currentGate = hit.collider.GetComponent<GateController>();

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
                    else if(currentGate != null)
                    {
                        currentGate.PlayAnimation();
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

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
    private PuzzleDoorController puzzleDoorController;
    private BasementDoorController basementDoorController;

    [SerializeField] private KeyCode openDoorKey = KeyCode.E;
    [SerializeField] private Image crosshair = null;
    private bool isCrosshairActive;
    private bool doOnce;

    private const string openTag = "Open";
    private const string lockedTag = "Locked";

    [SerializeField] private ProgressSystem progressSystem;
    [SerializeField] private ProgressNoteData noteDataD_currentDoor;
    [SerializeField] private ProgressNoteData noteDataS_currentDoor;
    [SerializeField] private ProgressNoteData noteDataP_currentDoor;
    [SerializeField] private ProgressNoteData noteDataB_currentDoor;

    private void Start()
    {
        inspectsystem = FindObjectOfType<InspectSystem>();
        singleDoorRay = FindObjectOfType<SingleDoorController>();
        doubleDoorRay = FindObjectOfType<DoubleDoorController>();
        gateController = FindObjectOfType<GateController>();
        puzzleDoorController = FindObjectOfType<PuzzleDoorController>();
        basementDoorController = FindObjectOfType<BasementDoorController>();
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
                PuzzleDoorController P_currentDoor = hit.collider.GetComponent<PuzzleDoorController>();
                BasementDoorController B_currentDoor = hit.collider.GetComponent<BasementDoorController>();

                if (Input.GetKeyDown(openDoorKey))
                {
                    if (D_currentDoor != null)
                    {
                        string keyRequired = D_currentDoor.GetRequiredKey();
                        if (inspectsystem.HasItem(keyRequired))
                        {
                            Debug.Log("Du lyckades öppnade dörren med nyckeln: " + keyRequired);
                            D_currentDoor.PlayAnimationDouble();
                            if (noteDataD_currentDoor != null && progressSystem != null)
                            {
                                foreach (string line in noteDataD_currentDoor.noteLines)
                                {
                                    Debug.Log($"Trying to cross out note: '{line}'");
                                    progressSystem.CrossOutNote(line);
                                }
                            }
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
                    else if (S_currentDoor != null)
                    {
                        string keyRequired = S_currentDoor.GetRequiredKey();
                        if (inspectsystem.HasItem(keyRequired))
                        {
                            Debug.Log("Du lyckades öppnade dörren med nyckeln: " + keyRequired);
                            S_currentDoor.PlayAnimationSingle();
                            if (noteDataS_currentDoor != null && progressSystem != null)
                            {
                                foreach (string line in noteDataS_currentDoor.noteLines)
                                {
                                    Debug.Log($"Trying to cross out note: '{line}'");
                                    progressSystem.CrossOutNote(line);
                                }
                            }
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
                    else if (P_currentDoor != null)
                    {
                        Debug.Log("Reached P_currentDoor interaction check.");
                        if (P_currentDoor.UnlockDoor())
                        {
                            Debug.Log("Puzzle door is unlocked. Playing animation.");
                            P_currentDoor.PlayAnimation();
                            if (noteDataP_currentDoor != null && progressSystem != null)
                            {
                                foreach (string line in noteDataP_currentDoor.noteLines)
                                {
                                    Debug.Log($"Trying to cross out note: '{line}'");
                                    progressSystem.CrossOutNote(line);
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("Puzzle door is still locked.");
                            ProgressNoteData noteData = P_currentDoor.GetComponentInParent<ProgressNoteData>();
                            if (noteData != null && !noteData.noteAlreadyAdded)
                            {
                                foreach (string line in noteData.noteLines)
                                {
                                    progressSystem.AddNote(line);
                                    noteData.noteAlreadyAdded = true;
                                }
                                P_currentDoor.EnablePaintingInteraction();
                            }
                        }
                    }
                    else if (B_currentDoor != null)
                    {
                        if (B_currentDoor.UnlockDoor())
                        {
                            Debug.Log("Basementdoor is unlocked. Playing animation.");
                            B_currentDoor.PlayAnimation();
                            if (noteDataB_currentDoor != null && progressSystem != null)
                            {
                                foreach (string line in noteDataB_currentDoor.noteLines)
                                {
                                    Debug.Log($"Trying to cross out note: '{line}'");
                                    progressSystem.CrossOutNote(line);
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("Basementdoor is still locked.");
                            ProgressNoteData noteData = B_currentDoor.GetComponentInParent<ProgressNoteData>();
                            if (noteData != null && !noteData.noteAlreadyAdded)
                            {
                                foreach (string line in noteData.noteLines)
                                {
                                    progressSystem.AddNote(line);
                                    noteData.noteAlreadyAdded = true;
                                }
                                B_currentDoor.EnableLeverInteraction();
                            }
                        }

                    }
                    else if (currentGate != null)
                    {
                        string keyRequired = currentGate.GetRequiredKey();

                        if (inspectsystem.HasItem(keyRequired))

                        {
                            Debug.Log("Gate is unlocked. Playing animation.");
                            currentGate.PlayAnimation();

                        }
                        else
                        {
                            Debug.Log("Gate is still locked.");
                            ProgressNoteData noteData = currentGate.GetComponentInParent<ProgressNoteData>();
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
            }
            // Hantering för öppna dörrar (spelar bara animationen)
            if (hit.collider.CompareTag(openTag))
            {
                DoubleDoorController D_currentDoor = hit.collider.GetComponent<DoubleDoorController>();
                SingleDoorController S_currentDoor = hit.collider.GetComponent<SingleDoorController>();
                GateController currentGate = hit.collider.GetComponent<GateController>();
                PuzzleDoorController P_currentDoor = hit.collider.GetComponent<PuzzleDoorController>();
                BasementDoorController B_currentDoor = hit.collider.GetComponent<BasementDoorController>();

                if (Input.GetKeyDown(openDoorKey))
                {
                    if (S_currentDoor != null) S_currentDoor.PlayAnimationSingle();
                    else if (D_currentDoor != null) D_currentDoor.PlayAnimationDouble();
                    else if (currentGate != null) currentGate.PlayAnimation();
                    else if (P_currentDoor != null)
                    {
                        P_currentDoor.PlayAnimation();
                        if (noteDataP_currentDoor != null && progressSystem != null)
                        {
                            foreach (string line in noteDataP_currentDoor.noteLines)
                            {
                                Debug.Log($"Trying to cross out note (open tag): '{line}'");
                                progressSystem.CrossOutNote(line);
                            }
                        }
                    }
                    else if (B_currentDoor != null) B_currentDoor.PlayAnimation();
                }
            }
        
    }
    //RaycastHit hit;
    //Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
    //int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

    //if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), forward, out hit, rayLenght, mask))
    //{
    //    if (hit.collider.CompareTag(lockedTag))
    //    {
    //        DoubleDoorController D_currentDoor = hit.collider.GetComponent<DoubleDoorController>();
    //        SingleDoorController S_currentDoor = hit.collider.GetComponent<SingleDoorController>();
    //        GateController currentGate = hit.collider.GetComponent<GateController>();
    //        PuzzleDoorController P_currentDoor = hit.collider.GetComponent<PuzzleDoorController>();
    //        BasementDoorController B_currentDoor = hit.collider.GetComponent<BasementDoorController>();


    //        if (D_currentDoor != null)
    //        {
    //            string keyRequired = D_currentDoor.GetRequiredKey();


    //            if (Input.GetKeyDown(openDoorKey))
    //            {
    //                if (inspectsystem.HasItem(keyRequired))
    //                {
    //                    Debug.Log("Du lyckades öppnade dörren med nyckeln: " + keyRequired);
    //                    D_currentDoor.PlayAnimationDouble();
    //                    if (noteDataD_currentDoor != null && progressSystem != null)
    //                    {
    //                        foreach (string line in noteDataD_currentDoor.noteLines)
    //                        {
    //                            progressSystem.CrossOutNote(line);
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    Debug.Log("Du behöver nyckeln: " + keyRequired);

    //                    ProgressNoteData noteData = D_currentDoor.GetComponentInParent<ProgressNoteData>();
    //                    if (noteData != null && !noteData.noteAlreadyAdded)
    //                    {
    //                        foreach (string line in noteData.noteLines)
    //                        {
    //                            progressSystem.AddNote(line);
    //                            noteData.noteAlreadyAdded = true;
    //                        }
    //                    }
    //                }
    //            }
    //        }

    //        if (S_currentDoor != null)
    //        {
    //            string keyRequired = S_currentDoor.GetRequiredKey();

    //            if (Input.GetKeyDown(openDoorKey))
    //            {
    //                if (inspectsystem.HasItem(keyRequired))
    //                {
    //                    Debug.Log("Du lyckades öppnade dörren med nyckeln: " + keyRequired);
    //                    S_currentDoor.PlayAnimationSingle();
    //                    if (noteDataS_currentDoor != null && progressSystem != null)
    //                    {
    //                        foreach (string line in noteDataS_currentDoor.noteLines)
    //                        {
    //                            progressSystem.CrossOutNote(line);
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    Debug.Log("Du behöver nyckeln: " + keyRequired);

    //                    ProgressNoteData noteData = S_currentDoor.GetComponentInParent<ProgressNoteData>();
    //                    if (noteData != null && !noteData.noteAlreadyAdded)
    //                    {
    //                        foreach (string line in noteData.noteLines)
    //                        {
    //                            progressSystem.AddNote(line);
    //                            noteData.noteAlreadyAdded = true;
    //                        }
    //                    }
    //                }
    //            }
    //        }

    //        if(P_currentDoor != null)
    //        {
    //            if (Input.GetKey(openDoorKey))
    //            {
    //                Debug.Log($"Trying to interact with puzzle door. P_currentDoor is {(P_currentDoor == null ? "null" : "not null")}");
    //                if (P_currentDoor.UnlockDoor())
    //                {
    //                    Debug.Log("Puzzle door is unlocked. Playing animation.");
    //                    P_currentDoor.PlayAnimation();
    //                    if (noteDataP_currentDoor != null && progressSystem != null)
    //                    {
    //                        foreach (string line in noteDataP_currentDoor.noteLines)
    //                        {
    //                            progressSystem.CrossOutNote(line);
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    Debug.Log("Puzzle door is still locked.");
    //                    ProgressNoteData noteData = P_currentDoor.GetComponentInParent<ProgressNoteData>();
    //                    if (noteData != null && !noteData.noteAlreadyAdded)
    //                    {
    //                        foreach (string line in noteData.noteLines)
    //                        {
    //                            progressSystem.AddNote(line);
    //                            noteData.noteAlreadyAdded = true;
    //                        }
    //                        P_currentDoor.EnablePaintingInteraction();
    //                    }
    //                }
    //            }

    //        }
    //        if (B_currentDoor != null)
    //        {
    //            if (Input.GetKey(openDoorKey))
    //            {
    //                Debug.Log($"Trying to interact with puzzle door. B_currentDoor is {(B_currentDoor == null ? "null" : "not null")}");
    //                if (B_currentDoor.UnlockDoor())
    //                {
    //                    Debug.Log("Basementdoor is unlocked. Playing animation.");
    //                    B_currentDoor.PlayAnimation();
    //                    if (noteDataB_currentDoor != null && progressSystem != null)
    //                    {
    //                        foreach (string line in noteDataB_currentDoor.noteLines)
    //                        {
    //                            progressSystem.CrossOutNote(line);
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    Debug.Log("Basementdoor is still locked.");
    //                    ProgressNoteData noteData = B_currentDoor.GetComponentInParent<ProgressNoteData>();
    //                    if (noteData != null && !noteData.noteAlreadyAdded)
    //                    {
    //                        foreach (string line in noteData.noteLines)
    //                        {
    //                            progressSystem.AddNote(line);
    //                            noteData.noteAlreadyAdded = true;
    //                        }
    //                        B_currentDoor.EnableLeverInteraction();
    //                    }
    //                }
    //            }

    //        }

    //        if (currentGate != null)
    //        {
    //            string keyRequired = currentGate.GetRequiredKey();

    //            if (Input.GetKey(openDoorKey))
    //            {
    //                if (inspectsystem.HasItem(keyRequired))
    //                {
    //                    Debug.Log("Du lyckades öppna grinden med nyckeln: " + keyRequired);
    //                    currentGate.PlayAnimation();
    //                }
    //            }
    //        }
    //    }
    //    if (hit.collider.CompareTag(openTag))
    //    {
    //        DoubleDoorController D_currentDoor = hit.collider.GetComponent<DoubleDoorController>();
    //        SingleDoorController S_currentDoor = hit.collider.GetComponent<SingleDoorController>();
    //        GateController currentGate = hit.collider.GetComponent<GateController>();
    //        PuzzleDoorController P_currentDoor = hit.collider.GetComponent<PuzzleDoorController>();
    //        BasementDoorController B_currentDoor = hit.collider.GetComponent<BasementDoorController>();


    //        if (Input.GetKeyDown(openDoorKey))
    //        {
    //            if (S_currentDoor != null)
    //            {
    //                S_currentDoor.PlayAnimationSingle();
    //            }
    //            else if (D_currentDoor != null)
    //            {
    //                D_currentDoor.PlayAnimationDouble();
    //            }
    //            else if(currentGate != null)
    //            {
    //                currentGate.PlayAnimation();
    //            }
    //            else if (P_currentDoor != null)
    //            {
    //                P_currentDoor.PlayAnimation();
    //            }
    //            else if (B_currentDoor != null)
    //            {
    //                B_currentDoor.PlayAnimation();
    //            }
    //        }
    //    }
    //}
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

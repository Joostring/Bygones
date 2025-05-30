// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Padlock : MonoBehaviour
{
    [SerializeField] private GameObject lockText;
    [SerializeField] private KeyCode enterCodeKey = KeyCode.E;
    [SerializeField] private InspectSystem inspectSystem;
    [SerializeField] private BoxController boxController;
    [SerializeField] private GameObject padlock;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject inputFieldObject;
    [SerializeField] private string correctCode = "9024";
    [SerializeField] private GameObject key;

    [SerializeField] private ProgressSystem progressSystem;
    [SerializeField] private ProgressNoteData progressNote;

    private string input;
    public bool boxOpen;
    private bool inReach;

    private void Start()
    {
        lockText.SetActive(false);
        inputFieldObject.SetActive(false);
        key.SetActive(false);
        inputField.onEndEdit.AddListener(SubmitCode);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !boxOpen)
        {
            inReach = true;
            lockText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inReach = false;
            lockText.SetActive(false);
            inputFieldObject.SetActive(false);
            inputField.DeactivateInputField();
        }
    }

    private void Update()
    {
        if (!boxOpen && inReach && Input.GetKeyDown(enterCodeKey))
        {
            inputFieldObject.SetActive(true);
            inputField.ActivateInputField();
            ProgressNoteData noteData = padlock.GetComponentInParent<ProgressNoteData>();
            if (noteData != null && progressSystem != null && noteData.noteAlreadyAdded == false)
            {
                foreach (string line in noteData.noteLines)
                {
                    progressSystem.AddNote(line);
                }
                noteData.noteAlreadyAdded = true;
            }
        }
        
        
    }

    private void SubmitCode(string enteredCode)
    {
        if (inReach && inputFieldObject.activeSelf)
        {
            if (enteredCode == correctCode)
            {
                ProgressNoteData noteData = padlock.GetComponentInParent<ProgressNoteData>();
                if (noteData != null && progressSystem != null)
                {
                    foreach (string line in noteData.noteLines)
                    {
                        progressSystem.CrossOutNote(line);
                    }
                }
                boxOpen = true;
                padlock.SetActive(false);
                boxController.PlayAnimation();
                inputFieldObject.SetActive(false);
                inputField.text = "";
                inputField.DeactivateInputField();
                lockText.SetActive(false);
                key.SetActive(true) ;
                Debug.Log("Padlock opened");
                
            }
            else
            {
                Debug.Log("Incorrect code");
                inputField.text = "";
                inputField.ActivateInputField();
            }
        }
    }
     

}

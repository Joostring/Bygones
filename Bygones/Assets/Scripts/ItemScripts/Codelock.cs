// Author : Jonas Östring

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Codelock : MonoBehaviour
{
    [SerializeField] private GameObject lockText;
    [SerializeField] private KeyCode enterCodeKey = KeyCode.E;
    [SerializeField] private InspectSystem inspectSystem;
    [SerializeField] private BoxController boxController;
    [SerializeField] private GameObject codelock;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject inputFieldObject;
    [SerializeField] private string correctCode = "864351";
    [SerializeField] private GameObject matchesObject;
    [SerializeField] private GameObject newspaperObject;
    [SerializeField] private ProgressSystem progressSystem;
    [SerializeField] private ProgressNoteData progressNote;
    private string input;
    public bool boxOpen;
    private bool inReach;

    private void Start()
    {
        lockText.SetActive(false);
        inputFieldObject.SetActive(false);
        inputField.onEndEdit.AddListener(SubmitCodeToCodelock);
        matchesObject.SetActive(false);
        newspaperObject.SetActive(false);
        
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
            ProgressNoteData noteData = inputFieldObject.GetComponentInParent<ProgressNoteData>();
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

    private void SubmitCodeToCodelock(string enteredCode)
    {
        if (inReach && inputFieldObject.activeSelf)
        {
            if (enteredCode == correctCode)
            {
                ProgressNoteData noteData = inputFieldObject.GetComponentInParent<ProgressNoteData>();
                if (noteData != null && progressSystem != null)
                {
                    foreach (string line in noteData.noteLines)
                    {
                        progressSystem.CrossOutNote(line);
                    }
                }
                boxOpen = true;                               
                inputFieldObject.SetActive(false);
                inputField.text = "";
                inputField.DeactivateInputField();
                lockText.SetActive(false);
                Debug.Log("Codelock opened");
                matchesObject.SetActive(true);
                newspaperObject.SetActive(true);
                boxController.PlayAnimation();

                
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

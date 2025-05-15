using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressSystem : MonoBehaviour
{

    //ADD SCRIPT TO PROGRESSSYSTEMOBJECT 
    
    [TextArea(3, 10)]
    public List<string> notes = new List<string>();
    public GameObject progressSystem;
    [SerializeField] private GameObject ProgressNoteData;
    public TMP_Text progressText;

    private bool isVisible = false;
   
    [SerializeField] private int waitTimer = 10;
    public void ToggleProgressViewUI()
    {
        
        if (progressSystem == null)
        {
            return;
        }

        if (progressText == null)
        {
            return;
        }

        isVisible = !isVisible;
        progressSystem.SetActive(isVisible);
        if (isVisible)
        {
            UpdateProgressText();
        }
    }

    public void HideProgressView()
    {
        if (progressSystem != null)
        {
            progressSystem.SetActive(false);
            isVisible = false;
        }
    }
    public void AddNote(string newNote)
    {
        
        Debug.Log("AddNote called with: " + newNote);
        if (string.IsNullOrWhiteSpace(newNote))
            return;

        notes.Add(newNote);
        UpdateProgressText();
        ShowNotification = true;

        //if (!isVisible) // OPEN LOGVIEW AUTOMATICALLY WHEN LOG IS ADDED
        //{
        //    ToggleProgressViewUI(); 
        //}
    }

    private void UpdateProgressText()
    {
        progressText.text = string.Join("\n\n", notes);
    }

    public bool ShowNotification { get; set; } = false;
    public bool IsVisible() => isVisible;


}

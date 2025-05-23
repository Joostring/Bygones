
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class ProgressSystem : MonoBehaviour
{
    //ADD SCRIPT TO PROGRESSSYSTEMOBJECT

    [TextArea(3, 10)]
    public List<string> notes = new List<string>();
    public GameObject progressSystemObject; // Döp om för att undvika förvirring med klassnamnet
    [SerializeField] private GameObject ProgressNoteData;
    public TMP_Text progressText;

    private bool isVisible = false;

    [SerializeField] private int waitTimer = 10;
    public void ToggleProgressViewUI()
    {
        if (progressSystemObject == null) 
        {
            return;
        }

        if (progressText == null)
        {
            return;
        }

        isVisible = !isVisible;
        progressSystemObject.SetActive(isVisible); 
        if (isVisible)
        {
            UpdateProgressText();
        }
    }

    public void HideProgressView()
    {
        if (progressSystemObject != null) 
        {
            progressSystemObject.SetActive(false); 
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
    }

    private void UpdateProgressText()
    {
        progressText.text = string.Join("\n\n", notes.Select(note => note.StartsWith("<crossed>") ? $"<color=black><s>{note.Substring(9)}</s></color>" : note));
    }

    /// <summary>
    /// Marks a note as crossed out. If the note exists, it will be updated in the list.
    /// </summary>
    /// <param name="noteToCross">The exact text of the note to cross out.</param>
    public void CrossOutNote(string noteToCross)
    {
        int index = notes.IndexOf(noteToCross);
        if (index != -1)
        {
            if (!notes[index].StartsWith("<crossed>"))
            {
                notes[index] = "<crossed>" + notes[index];
                UpdateProgressText();
            }
        }
        else
        {
            Debug.LogWarning($"Note not found: {noteToCross}");
        }
    }

    /// <summary>
    /// Deletes a specific note from the progress system.
    /// </summary>
    /// <param name="noteToDelete">The exact text of the note to delete.</param>
    public void DeleteNote(string noteToDelete)
    {
        if (notes.Remove(noteToDelete))
        {
            UpdateProgressText();
        }
        else
        {
            Debug.LogWarning($"Note not found for deletion: {noteToDelete}");
        }
    }

    public bool ShowNotification { get; set; } = false;
    public bool IsVisible() => isVisible;
}

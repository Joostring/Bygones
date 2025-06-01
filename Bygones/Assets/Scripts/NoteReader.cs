// Author : Ylva Sundblad

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteReader : MonoBehaviour
{
    [TextArea(3, 10)]
    public string noteText;

    public GameObject noteReaderUI;
    public TMP_Text noteTextUI; 

    private bool isNoteVisible = false;

    public void ToggleNoteUI()
    {
        Debug.Log("ToggleNoteUI() CALLED");

        //if (noteReaderUI == null || noteTextUI == null) return;
        if (noteReaderUI == null)
        {
            Debug.LogError("noteReaderUI is NOT assigned!");
            return;
        }

        if (noteTextUI == null)
        {
            Debug.LogError("noteTextUI is NOT assigned!");
            return;
        }

        isNoteVisible = !isNoteVisible;
        noteReaderUI.SetActive(isNoteVisible);
        Debug.Log("UI SetActive: " + isNoteVisible);
        if (isNoteVisible)
        {
            noteTextUI.text = noteText;
            Debug.Log("Text set to: " + noteText);
        }
    }

    public void HideNote()
    {
        if (noteReaderUI != null)
        {
            noteReaderUI.SetActive(false);
            isNoteVisible = false;
        }
    }

    public bool IsVisible() => isNoteVisible;
}

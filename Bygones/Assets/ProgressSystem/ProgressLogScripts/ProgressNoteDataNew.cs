//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ProgressNoteData : MonoBehaviour
//{
//    [TextArea(3, 10)]
//    public List<string> noteLines;

//    public bool noteAlreadyAdded = false;

//    /// <summary>
//    /// Adds the notes in noteLines to the ProgressSystem if they haven't been added before.
//    /// </summary>
//    public void AddNotesToSystem()
//    {
//        if (!noteAlreadyAdded)
//        {
//            ProgressSystem progressSystem = FindObjectOfType<ProgressSystem>();
//            if (progressSystem != null)
//            {
//                foreach (string line in noteLines)
//                {
//                    progressSystem.AddNote(line);
//                }
//                noteAlreadyAdded = true;
//            }
//            else
//            {
//                Debug.LogError("ProgressSystem not found in the scene.");
//            }
//        }
//    }

//    /// <summary>
//    /// Marks all notes from this ProgressNoteData as crossed out in the ProgressSystem.
//    /// </summary>
//    public void CrossOutNotesInSystem()
//    {
//        ProgressSystem progressSystem = FindObjectOfType<ProgressSystem>();
//        if (progressSystem != null)
//        {
//            foreach (string line in noteLines)
//            {
//                progressSystem.CrossOutNote(line);
//            }
//        }
//        else
//        {
//            Debug.LogError("ProgressSystem not found in the scene.");
//        }
//    }

//    /// <summary>
//    /// Deletes all notes from this ProgressNoteData from the ProgressSystem.
//    /// </summary>
//    public void DeleteNotesFromSystem()
//    {
//        ProgressSystem progressSystem = FindObjectOfType<ProgressSystem>();
//        if (progressSystem != null)
//        {
//            foreach (string line in noteLines)
//            {
//                progressSystem.DeleteNote(line);
//            }
//        }
//        else
//        {
//            Debug.LogError("ProgressSystem not found in the scene.");
//        }
//    }
//}
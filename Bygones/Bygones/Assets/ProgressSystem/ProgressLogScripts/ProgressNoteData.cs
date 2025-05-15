using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressNoteData : MonoBehaviour
{
    [TextArea(3, 10)]
    public List<string> noteLines;

    public bool noteAlreadyAdded = false;
}

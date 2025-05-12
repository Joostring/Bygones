using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalBoxController : MonoBehaviour
{
    [SerializeField] private GameObject[] lamps;
    [SerializeField] private GameObject text;
    [SerializeField] private KeyCode lightKey = KeyCode.E;
    [SerializeField] public InspectSystem inspectsystem;

    public bool powerOff;
    private bool inReach;
    private bool isClosed;

   [SerializeField] private Animator electricalBoxAnim;
    [SerializeField] private Animator knob01;

    [Header("Animation Names")]
    [SerializeField] private string openAnimationName = "ElectricalBoxOpen";
    [SerializeField] private string turnKnob = "TurnKnob";
    public ProgressSystem progressSystem;
    [SerializeField] private GameObject electricalBox;

    void Start()
    {
        powerOff = true;
        isClosed = true;
        foreach (GameObject go in lamps) { go.SetActive(false); }
        text.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && powerOff)
        {
            inReach = true;
            text.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inReach = false;
            text.SetActive(false);
        }
    }

    void Update()
    {
        if (isClosed && inReach && Input.GetKeyDown(lightKey))
        {
            electricalBoxAnim.Play(openAnimationName, 0, 0.0f);
            isClosed = false;

        }
        else if (!isClosed && powerOff && inReach && Input.GetKeyDown(lightKey) && inspectsystem.HasItem("Flashlight_Inspect"))
        {
            powerOff = false;

            foreach (GameObject go in lamps)
            {
                go.SetActive(true);
                knob01.Play(turnKnob, 0, 0.0f);
            }

        }
        else if (!isClosed && powerOff && inReach && Input.GetKeyDown(lightKey)) 
        {
            // PROGRESS NOTES ADDED WHEN TRYING TO OPEN LOCKED DOOR -----------------

            ProgressNoteData noteData = electricalBox.GetComponentInParent<ProgressNoteData>();
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
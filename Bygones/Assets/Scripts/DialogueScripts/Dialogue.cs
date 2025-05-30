using System.Collections;
using UnityEngine;
using TMPro;

//Written by Jennifer

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    [SerializeField] float textSpeed;
    [SerializeField] float lookDistance;
    [SerializeField] float dialogueCooldown;

    private bool dialogueStarted = false;
    private float lastDialogueTime = -Mathf.Infinity;

    public string[] lines;
    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
        textComponent.gameObject.SetActive(false);
    }

    void Update()
    {
        //If the dialogue hasn't started and the time is longer than the last dialogue + the dialogue cooldown it'll play the dialogue
        if (!dialogueStarted && Time.time >= lastDialogueTime + dialogueCooldown)
        {
            if (LookingAtObject())
            {
                dialogueStarted = true;
                StartDialogue();
            }
        }
    }

    bool LookingAtObject()
    {
        //Checks if the player is looking at an object with dialogue
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, lookDistance))
        {
            return hit.transform == transform;
        }

        return false;
    }

    void StartDialogue()
    {
        //Starts dialogue
        index = 0;
        textComponent.gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }
    void EndDialogue()
    {
        //Ends dialogue
        textComponent.gameObject.SetActive(false);
        dialogueStarted = false;
        lastDialogueTime = Time.time;
    }

    IEnumerator TypeLine()
    {
        //Empties the string then begins writing the dialogue written in inspector one letter at a time
        textComponent.text = string.Empty;

        foreach (char character in lines[index].ToCharArray())
        {
            textComponent.text += character;
            yield return new WaitForSeconds(textSpeed);
        }

        //Waits a while before moving on to next line or ending the dialogue
        yield return new WaitForSeconds(1f);

        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI textComponent;
    public Camera playerCam;
    public string[] lines;
    public float textSpeed;

    private int index;
    private bool dialogueStarted = false;

    private float inputCooldown = 0.2f;
    private float lastInputTime = 0f;

    void Start()
    {
        textComponent.text = string.Empty;
        playerCam = GameObject.Find("PlayerCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!dialogueStarted)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
                
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        dialogueStarted = true;
                        StartDialogue();
                    }
                }
            }
        }
        else
        {
            if (Time.time - lastInputTime > inputCooldown && Input.GetKey(KeyCode.Space))
            {
                lastInputTime = Time.time;

                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());  
        }
        else
        {
            textComponent.gameObject.SetActive(false);
        }
    }
}

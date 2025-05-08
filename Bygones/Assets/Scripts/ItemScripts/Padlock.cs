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
    //[SerializeField] private InputfieldPadlock inputfieldPadlock;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject inputFieldObject;

    private string input;
    public bool boxOpen;
    private bool inReach;

    private void Start()
    {
        lockText.SetActive(false);
        inputFieldObject.SetActive(false);
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
        }
    }

    private void Update()
    {
        if (!boxOpen && inReach && Input.GetKeyDown(enterCodeKey))
        {
            inputFieldObject.SetActive(true);

            if(inputField.text == "9024") 
            {
                boxOpen = true;
                padlock.SetActive(false);
                boxController.PlayAnimation();
                inputFieldObject.SetActive(false);
                lockText.SetActive(false);
            }
            else
            {
                boxOpen = false;
                padlock.SetActive(true);
                inputFieldObject.SetActive(true);
            }
            

        }
        
    }
    

    public void CheckInput(string s)
    {
        input = s;
        Debug.Log(input);
        //if (inputField.text == "9024")
        //{
        //    Debug.Log("Code correct");
        //}
    }

}

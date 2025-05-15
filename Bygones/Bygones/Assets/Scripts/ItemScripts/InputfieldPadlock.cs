using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputfieldPadlock : MonoBehaviour
{
    //public InputField inputField;
    public TMP_InputField inputField;


    public void CheckInput()
    {
        if (inputField.text == "9024")
        {
            Debug.Log("Code correct");
        }
    }
}

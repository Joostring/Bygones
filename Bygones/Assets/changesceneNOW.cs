using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changesceneNOW : MonoBehaviour
{
   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            SceneManager.LoadScene(2);
        }


    }
}

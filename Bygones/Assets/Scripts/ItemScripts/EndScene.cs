// Author : Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    [SerializeField] Crossfade crossfade;
    [SerializeField] private InspectSystem inspectSystem;
    [SerializeField] private int endScene = 4;





    private void Update()
    {
        if (inspectSystem.HasItem("Note_Final_Inspect"))
        {
            Debug.Log("Final note found. Loading next scene.");
            crossfade.LoadScene(endScene);
        }
        else
        {
            Debug.Log("Final note NOT found.");
        }
    }
}

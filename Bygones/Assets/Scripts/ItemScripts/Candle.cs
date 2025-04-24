// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{

    [SerializeField] private GameObject[] flames;
    [SerializeField] private GameObject lightText;
    [SerializeField] private KeyCode lightKey = KeyCode.E;
    [SerializeField] public InspectSystem inspectsystem;

    public bool unLit;
    private bool inReach;

    void Start()
    {
        unLit = true;
        foreach (GameObject go in flames) { go.SetActive(false); }
        lightText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && unLit )
        {
            inReach = true;
            lightText.SetActive(true);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && unLit)
        {
            inReach = false;
            lightText.SetActive(false);
        }
    }

    void Update()
    {
        if (unLit && inReach && Input.GetKeyDown(lightKey) && inspectsystem.HasItem("Matches_Inspect"))
        {
            unLit = false;

            foreach (GameObject go in flames) 
            {
                go.SetActive(true);
                
            }
            
        }
    }
}

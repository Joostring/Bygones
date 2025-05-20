// Author : Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    [SerializeField] Crossfade crossfade;

    [SerializeField] KeyCode interact = KeyCode.E;
    private bool inReach = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = true;
        }
    }

    private void Update()
    {
        if (inReach && Input.GetKeyDown(interact))
        {
            crossfade.LoadNextScene();
        }
    }
}

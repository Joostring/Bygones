using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class TriggerItemFlashBack : MonoBehaviour
{
    [SerializeField] MoveObjectUp moveUp;
    public Transform interactorSource;
    public float interactRange = 3f;
    public LayerMask interactableLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(interactorSource.position, interactorSource.forward);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo, interactRange, interactableLayer))
            {
                moveUp.StartRise();
            }
        }
    }
}

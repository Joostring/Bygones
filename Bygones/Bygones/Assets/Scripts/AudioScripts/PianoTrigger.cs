using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoTrigger : MonoBehaviour
{
    [SerializeField] private GameObject pianoObject;

    private void Start()
    {
        pianoObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (pianoObject.CompareTag("Piano") && pianoObject != null)
            {
                pianoObject.SetActive(true);

            }

        }
    }
}

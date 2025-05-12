using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhisperTrigger : MonoBehaviour
{
    [SerializeField] private GameObject whisperObject;

    private void Start()
    {
        whisperObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (whisperObject.CompareTag("Whisper") && whisperObject != null)
            {
                whisperObject.SetActive(true);

            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateTriggers : MonoBehaviour
{
    [SerializeField] private GameObject[] triggers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var trigger in triggers)
            {
                if (trigger != null) // Add null check to avoid errors
                {
                    trigger.SetActive(false);
                }
                else
                {
                    Debug.LogWarning("One of the triggers in the array is null on " + gameObject.name);
                }
            }
        }
    }
}

// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseTrigger : MonoBehaviour
{
    [SerializeField] private GameObject vaseObject;

    private void Start()
    {
        vaseObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (vaseObject.CompareTag("Vase") && vaseObject != null)
            {
                vaseObject.SetActive(true);

            }

        }
    }
}

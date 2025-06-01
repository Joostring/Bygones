// Author : Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateHealing : MonoBehaviour
{
    [SerializeField] private GameObject healingZone;
    [SerializeField] private GameObject lightSource;

    private void Start()
    {
        healingZone.SetActive(false);
    }

    private void Update()
    {
        if (lightSource.activeInHierarchy)
        {
            healingZone.SetActive (true);
        }
    }
}

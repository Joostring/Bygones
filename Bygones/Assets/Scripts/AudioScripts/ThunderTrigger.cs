using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderTrigger : MonoBehaviour
{
    [SerializeField] private GameObject thunderObject;
    [SerializeField] private float thunderTime = 2f;

    private void Start()
    {
        thunderObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (thunderObject.CompareTag("Thunder") && thunderObject != null)
            {
                thunderObject.SetActive(true);
                Invoke("DeactivateThunder", thunderTime);
            }
            

        }
    }

    private void DeactivateThunder()
    {
        if (thunderObject != null)
        {
            thunderObject.SetActive(false);
        }
    }
}

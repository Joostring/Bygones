using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCandle : MonoBehaviour
{
    [SerializeField] private int rayLenght = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;

    private Candle candle;

    [SerializeField] private KeyCode lightUp = KeyCode.E;

    private const string litTag = "Lit";
    private const string unlitTag = "Unlit";

    private void Start()
    {
        candle = GetComponent<Candle>();
    }


    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        //Debug.DrawRay(transform.position + new Vector3(0,1,0), forward, Color.green);
        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), forward, out hit, rayLenght, mask))
        {
            Debug.Log("Skickar ray");

            if (candle != null)
            {
                //string itemRequired = candle.GetItem();

                if (Input.GetKeyDown(lightUp))
                {
                    Debug.Log("försöker tända");
                    //candle.Light();
                }
            }
            //if (hit.collider.CompareTag(unlitTag))
            //{
            //    Candle candle = hit.collider.GetComponent<Candle>();
            //    Debug.Log("Träffar");

            //    if (candle != null)
            //    {
            //        string itemRequired = candle.GetItem();

            //        if (Input.GetKeyDown(lightUp))
            //        {
            //            Debug.Log("försöker tända");
            //            candle.Light();
            //        }
                //  }
            //}

        }
    }
}

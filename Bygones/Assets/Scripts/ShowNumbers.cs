// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowNumbers : MonoBehaviour
{
    [SerializeField] private Candle candle;
    [SerializeField] private MeshRenderer numbers;
   
    void Start()
    {       
        numbers.enabled = false;       
    }
   
    void Update()
    {
        if (candle.unLit == false) {numbers.enabled = true;}
    }
}

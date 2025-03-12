using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pills : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LowSanityTimer lowSanityTimer;
    [SerializeField] float sanityRestore;
    [SerializeField] float pillTimer = 0;
    [SerializeField] float pillTimerReduce = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pillTimer = Mathf.Clamp(pillTimer, 0, 100);

        pillTimer -= pillTimerReduce * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.K))
        {
            //lowSanityTimer.PillSanityGain(10);
            pillTimer += 100;
         
            if (pillTimer <= 50)
            {
                lowSanityTimer.PillSanityGain(1);
            }
            else
            {
                lowSanityTimer.PillSanityGain(10);
            }
        }
    }
}

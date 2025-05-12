using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectUp : MonoBehaviour
{
    [SerializeField] float riseSpeed = 0f;
    [SerializeField] float riseDistance = 0f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isRising = false;
    private bool hasRisen = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.up * riseDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRising)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, riseSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isRising = false;
                hasRisen = true;
            }
        }
    }
    public void StartRise()
    {
        if (!isRising && !hasRisen)
        {
            isRising = true;
        }
    }
}

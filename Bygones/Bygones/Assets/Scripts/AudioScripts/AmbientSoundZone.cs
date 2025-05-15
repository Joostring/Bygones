// Author Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSound : MonoBehaviour // Author Jonas Östring
{
    [Tooltip("Area where to play the sound")]
    public Collider area;
    [Tooltip("GameObject to follow")]
    public GameObject player; // The GameObject who hold the audiolistener
    
    
    void Update()
    {
        Vector3 closestPoint = area.ClosestPoint(player.transform.position);
        transform.position = closestPoint;
    }
}

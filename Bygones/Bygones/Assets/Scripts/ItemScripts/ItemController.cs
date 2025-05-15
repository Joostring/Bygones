using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item item;

    private void Awake()
    {
        item = GetComponent<Item>();
    }
}

// Author : Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    [SerializeField] private GameObject paintingText;
    [SerializeField] private KeyCode turnKey = KeyCode.E;
    [SerializeField] private PaintingController paintingController;
    [SerializeField] private PuzzleDoorController puzzleDoorController;


    public bool isTurned;
    private bool inReach;

    private void Start()
    {
        paintingText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (puzzleDoorController != null && puzzleDoorController.paintingIsActive && other.CompareTag("Player") && !isTurned)
        {
            inReach = true;
            paintingText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = false;
            paintingText.SetActive(false);
        }
    }

    private void Update()
    {
        if (puzzleDoorController != null && puzzleDoorController.paintingIsActive && inReach && !isTurned && Input.GetKeyDown(turnKey))
        {
            isTurned = true;
            Debug.Log($"{gameObject.name} turned: {isTurned}");
            paintingController.PlayAnimation();
            puzzleDoorController.CheckAndUnlockDoor();
        }
    }
}

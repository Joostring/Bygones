using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
    [SerializeField] private GameObject drawerText;
    [SerializeField] private KeyCode openKey = KeyCode.E;
    [SerializeField] private InspectSystem inspectsystem;
    [SerializeField] private BoxController boxController;

    public bool open;
    private bool inReach;

    private void Start()
    {
        open = false;
        drawerText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !open)
        {
            inReach = true;
            drawerText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = false;
            drawerText.SetActive(false);
        }
    }

    private void Update()
    {
        if (inReach && !open && Input.GetKeyUp(openKey))
        {
            open = true;
            boxController.PlayAnimation();
            Destroy(this);
        }
    }
}

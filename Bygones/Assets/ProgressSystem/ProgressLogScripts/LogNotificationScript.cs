// Author Ylva Sundblad

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogNotificationScript : MonoBehaviour
{
    [SerializeField] private GameObject LogText;
    [SerializeField] private int waitTimer = 2;
    [SerializeField] private ProgressSystem progressSystem;
    private bool isNotificationRunning = false;

    void Update()
    {
        
        if (progressSystem != null && progressSystem.ShowNotification && !isNotificationRunning)
        {
            StartCoroutine(ShowNotification());
            progressSystem.ShowNotification = false;
        }
    }
    private IEnumerator ShowNotification()
    {
        Debug.Log("Reached IEnumerator");
        isNotificationRunning = true; // S�tt till true n�r coroutinen startar
        LogText.SetActive(true);
        yield return new WaitForSeconds(waitTimer);
        LogText.SetActive(false);
        isNotificationRunning = false; // S�tt tillbaka till false n�r den �r klar
        
    }
}

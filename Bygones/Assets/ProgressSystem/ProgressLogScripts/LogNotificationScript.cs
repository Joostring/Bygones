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
        //Debug.Log("LogNotificationScript Update called");
        //Debug.Log("progressSystem != null: " + (progressSystem != null));
        //Debug.Log("progressSystem.ShowNotification: " + progressSystem.ShowNotification);
        //Debug.Log("!isNotificationRunning: " + (!isNotificationRunning));

        //if (progressSystem != null && progressSystem.ShowNotification && !isNotificationRunning)
        //{
        //    Debug.Log("Starting ShowNotification coroutine");
        //    StartCoroutine(ShowNotification());
        //    progressSystem.ShowNotification = false;
        //}
        if (progressSystem != null && progressSystem.ShowNotification && !isNotificationRunning)
        {
            StartCoroutine(ShowNotification());
            progressSystem.ShowNotification = false;
        }
    }
    private IEnumerator ShowNotification()
    {
        Debug.Log("Reached IEnumerator");
        isNotificationRunning = true; // Sätt till true när coroutinen startar
        LogText.SetActive(true);
        yield return new WaitForSeconds(waitTimer);
        LogText.SetActive(false);
        isNotificationRunning = false; // Sätt tillbaka till false när den är klar
        //Debug.Log("Reached IEnumerator");
        //LogText.SetActive(true);
        //yield return new WaitForSeconds(waitTimer);
        //LogText.SetActive(false);
    }
}

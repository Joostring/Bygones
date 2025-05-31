// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crossfade : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;
    //[SerializeField] private GameObject basementDoor;
    //[SerializeField] private bool isOpen = false;
    //[SerializeField] private InspectSystem inspectSystem;


    //private void Awake()
    //{
    //    inspectSystem = GetComponent<InspectSystem>();
    //}

    void Update()
    {
        //if (inspectSystem.HasItem("Basement_Key"))
        //{
        //    isOpen = true;
        //}
        //if (Input.GetKeyDown(KeyCode.L) && isOpen && inspectSystem.HasItem("Key_Front_Inspect" ))
        //{
        //    LoadNextScene();
        //}

    }

    //public void LoadNextScene()
    //{
    //    StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
       
    //}

    // IEnumerator LoadScene(int sceneIndex)
    //{
    //    transition.SetTrigger("Start");
    //    yield return new WaitForSeconds(transitionTime);
    //    SceneManager.LoadScene(sceneIndex);
    //}

    public void LoadScene(int sceneIndex)
    {
        Debug.Log("Crossfade: Starting load of scene " + sceneIndex);
        StartCoroutine(LoadSceneCoroutine(sceneIndex));
    }

    IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        Debug.Log("Crossfade: Loading scene " + sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }
}

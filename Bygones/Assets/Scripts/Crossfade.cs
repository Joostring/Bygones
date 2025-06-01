// Author: Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crossfade : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;
    
  

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

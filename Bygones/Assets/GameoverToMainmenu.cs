// Author : Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverToMainmenu : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitTransitionScene());

    }


    public IEnumerator WaitTransitionScene()
    {
        yield return new WaitForSeconds(20);
        SceneManager.LoadScene(0);

    }
}

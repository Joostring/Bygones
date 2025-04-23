using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneAfterXTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitTransitionScene());

    }


    public IEnumerator WaitTransitionScene()
    {
        yield return new WaitForSeconds(83);
        SceneManager.LoadScene(2);

    }

}

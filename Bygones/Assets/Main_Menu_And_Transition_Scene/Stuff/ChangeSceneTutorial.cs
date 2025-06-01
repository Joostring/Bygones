using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTutorial : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitForScene());
    }

    public IEnumerator WaitForScene()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(3);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
   public void ButtonStart()
    {
        StartCoroutine(LoadNextScen());
        Debug.Log("test");
    }

    public IEnumerator LoadNextScen()
    {
        Debug.Log("Startar coroutine");
        yield return new WaitForSeconds(2);
        Debug.Log("Försöker ladda scen 1");
        SceneManager.LoadScene(1);
    }
}

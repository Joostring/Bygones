using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitTransitionScene());

    }


    public IEnumerator WaitTransitionScene()
    {
        yield return new WaitForSeconds(115);
        SceneManager.LoadScene(0);

    }
}

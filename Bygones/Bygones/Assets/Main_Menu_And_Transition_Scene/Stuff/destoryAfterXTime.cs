using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class destoryAfterXTime : MonoBehaviour
{
    // Start is called before the first frame update
    public int time;
    public GameObject textobject;
    void Start()
    {
        StartCoroutine(destoryAfterX(time));
    }

    public IEnumerator destoryAfterX(int timer)
    {
        yield return new WaitForSeconds(timer);
        textobject.SetActive(false);

    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sanity_ui : MonoBehaviour
{
    [SerializeField] Slider sanityBar;
    [SerializeField] LowSanityTimer sanityScript; 

    bool hasLoadedScene = false;

    void Update()
    {
        if (sanityScript != null)
        {
            float sanity = sanityScript.GetSanity();
            sanityBar.value = sanity; 

            if (sanity <= 0f && !hasLoadedScene)
            {
                hasLoadedScene = true;
                SceneManager.LoadScene("DeathScene"); 
            }
        }
    }
}

// Author : Jonas Östring

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [SerializeField] private LowSanityTimer sanity;
    [SerializeField] private Crossfade crossfade;
    [SerializeField] private int deathSceneIndex = 5; // Make the death scene index configurable

    private void Update()
    {
        if (sanity != null)
        {
            if (sanity.sanityProcentage <= 0)
            {
                if (crossfade != null)
                {
                    crossfade.LoadScene(deathSceneIndex); // Use the Crossfade's LoadScene method
                }
                else
                {
                    SceneManager.LoadScene(deathSceneIndex);
                }
            }
        }
    }
}
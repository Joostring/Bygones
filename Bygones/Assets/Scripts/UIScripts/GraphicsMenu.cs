using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


//Made by Jennifer

public class GraphicsMenu : MonoBehaviour
{
    [SerializeField] Slider mouseSensitivitySlider;
    [SerializeField] Slider FOVSlider;
    [SerializeField] TMP_Dropdown resolutionDropdown;

    GameObject playerObject;
    [SerializeField] Camera playerCamera;

    Resolution[] resolutions;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        PopulateResolution();
    }

    void Update()
    {
        
    }

    public void ChangeSensitivity()
    {
        int newSensitivity = (int)mouseSensitivitySlider.value;
        playerObject.GetComponent<PlayerLook>().mouseSensitivity = newSensitivity;
    }

    public void ChangeFOV()
    {
        int newFOV = (int)FOVSlider.value;
        playerCamera.fieldOfView = newFOV;
    }

    public void ChangeResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void PopulateResolution()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;

            if  (!options.Contains(option))
            {
                options.Add(option);
            }

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }
}

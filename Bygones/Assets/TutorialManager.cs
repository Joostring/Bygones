using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialPanels;
    public Button nextButton;

    int currentPanelIndex = 0;
    public static bool IsTutorialActive { get; private set; } = false;
    public static event Action<bool> OnTutorialStateChanged;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        foreach (var panel in tutorialPanels)
            panel.SetActive(false);

        if (tutorialPanels.Length == 0)
        {
            EndTutorial();
            return;
        }

        tutorialPanels[0].SetActive(true);
        IsTutorialActive = true;
        OnTutorialStateChanged?.Invoke(true);

        if (nextButton != null)
            nextButton.onClick.AddListener(ShowNextPanel);
    }

    public void ShowNextPanel()
    {
        tutorialPanels[currentPanelIndex].SetActive(false);
        currentPanelIndex++;

        if (currentPanelIndex < tutorialPanels.Length)
        {
            tutorialPanels[currentPanelIndex].SetActive(true);
        }
        else
        {
            EndTutorial();
        }
    }

    void EndTutorial()
    {
        if (nextButton != null)
            nextButton.gameObject.SetActive(false);

        IsTutorialActive = false;
        OnTutorialStateChanged?.Invoke(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(3);
    }
}

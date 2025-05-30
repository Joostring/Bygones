using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Made by Jennifer

public class PauseMenu : MonoBehaviour
{
    bool isPaused = false;

    GameObject playerObject; 
    [SerializeField] GameObject pauseMenuObject; 
    [SerializeField] GameObject pauseStartMenuObject;
    [SerializeField] GameObject menuList;
    [SerializeField] GameObject screenCursor;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        ResetPauseUI();
    }

    public void OnPauseGame(InputValue inputValue)
    {
        Debug.Log("OnPauseGameCalled");
        if (!isPaused) { PauseGame(); }
        else if (isPaused) { UnPauseGame(); }
    }
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        playerObject.GetComponent<PlayerInput>().SwitchCurrentActionMap("UIActionMap");
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        pauseMenuObject.SetActive(true);
        screenCursor.SetActive(false);
    }
    public void UnPauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        playerObject.GetComponent<PlayerInput>().SwitchCurrentActionMap("PlayerActionMaps");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pauseMenuObject.SetActive(false);
        screenCursor.SetActive(true);
        ResetPauseUI();
    }
    public void ResetPauseUI()
    {
        for (int i = 0; i < menuList.transform.childCount; i++)
        {
            menuList.transform.GetChild(i).gameObject.SetActive(false);
        }
        pauseStartMenuObject.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}

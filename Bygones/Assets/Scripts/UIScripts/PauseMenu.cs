using UnityEngine;
using UnityEngine.InputSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif

//Made by Jennifer

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;

    GameObject playerObject;
    [SerializeField] GameObject pauseMenuObject;
    [SerializeField] GameObject pauseStartMenuObject;
    [SerializeField] GameObject menuList;
    [SerializeField] GameObject screenCursor;
    public InspectSystem InspectSystem;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        ResetPauseUI();
    }

    public void OnPauseGame(InputValue inputValue)
    {
        if (InspectSystem != null && InspectSystem.isInspecting)
        {
            return;
        }

        if (InspectSystem.isInventoryOpen == true)
        {

            return;
        }

        if (!isPaused)
        {
            PauseGame();
        }
        else
        {
            UnPauseGame();
        }
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
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}

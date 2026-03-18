using UnityEngine;
using UnityEngine.SceneManagement;

public class HubPauseUI : MonoBehaviour
{
    [SerializeField] private GameObject hubMenu; 
    private bool isPaused = false;

    private void Update()
    {
        // Press P to toggle pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        hubMenu.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        hubMenu.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
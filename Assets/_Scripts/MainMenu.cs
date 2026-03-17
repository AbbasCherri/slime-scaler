using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Hub"); // Name of Hub scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

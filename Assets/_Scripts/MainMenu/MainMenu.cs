using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("First_Level"); // Name of Hub scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

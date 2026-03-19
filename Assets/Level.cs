using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] int totalGems = 5;
    [SerializeField] private int collectedGems = 0;

    void Awake()
    {
        Instance = this;
    }

    public void RegisterGem()
    {
        totalGems++;
    }

    public void CollectGem()
    {
        collectedGems++;

        if (collectedGems >= totalGems)
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
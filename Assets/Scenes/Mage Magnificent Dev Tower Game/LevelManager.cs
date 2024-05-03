using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public string[] levelScenes = { "Level1", "Level2", "Level3", "Level4", "Level5", "Level6", "Level7", "Level8", "Level9", "Level10" };

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex < levelScenes.Length - 1)
        {
            SceneManager.LoadScene(levelScenes[currentSceneIndex + 1]);
        }
        else
        {
            Debug.Log("Reached the last level.");
        }
    }

    public void LoadLevel(string levelName)
    {
        if (SceneManager.GetSceneByName(levelName).isLoaded)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelName));
        }
        else
        {
            Debug.LogError("Level not found: " + levelName);
        }
    }
}
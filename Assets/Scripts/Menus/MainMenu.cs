using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// This creates a new game
    /// </summary>
    public void PlayGame ()
    {
        //Need to add "Game" scene to build settings in unity after the 'MainMenu' for this to function, waiting on game creation
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GlobalData.reset();
        Time.timeScale = 1f;
    }

    public void QuitGame ()
    {
        //Logging Quit Game for Unity Console
        Debug.Log("Game Quit");
        Application.Quit();
    }


    public void Continue()
    {
        SaveSystem.loadPlayer();
        SceneManager.LoadScene(GlobalData.currentSceneIndex);
        Time.timeScale = 1f;
    }
}

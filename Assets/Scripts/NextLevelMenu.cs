using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{

    public static bool LevelIsNext = false;
    public GameObject levelMenuUI;

    // Update is called once per frame
    void UpdateNext () {
 
        Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").Length);
 
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Next();
        }
    }

    void Next ()
    {
        levelMenuUI.SetActive(true);
        Time.timeScale = 0f;
        LevelIsNext = true;
    }

    public void NextLevelButton ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
        LevelIsNext = false;
    }

    public void returnToMainNext ()
    {
        SceneManager.LoadScene(0);
        LevelIsNext = false;
        Time.timeScale = 1f;
    }
}

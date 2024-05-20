using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{

    public static bool LevelIsNext = false;
    public GameObject levelMenuUI;

    private bool addXP = false;

    // Update is called once per frame
    void Update () {
 
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Item").Length == 0)
        {
            if (!addXP)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                if (players.Length > 0)
                {
                    players[0].GetComponent<Player>().addExperience(2); ;
                }
                addXP = true;
            }
            

            Next();
        }
    }

    void Next ()
    {
        levelMenuUI.SetActive(true);
        LevelIsNext = true;
    }

    public void NextLevelButton ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        LevelIsNext = false;
    }

    public void returnToMainNext ()
    {
        SceneManager.LoadScene(0);
        LevelIsNext = false;
    }
}

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
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Player player = players[0].GetComponent<Player>();
        GlobalData.playerMaxHealth = player.maxHealth;
        GlobalData.playerMaxMana = player.maxMana;
        GlobalData.playerXP = player.getExperience();
        GlobalData.playerLevel = player.getLevel();
 
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Item").Length == 0)
        {
            if (!addXP)
            {

                if (players.Length > 0)
                {
                    player.addExperience(2);
                    GlobalData.playerMaxHealth = player.maxHealth;
                    GlobalData.playerMaxMana = player.maxMana;
                    GlobalData.playerXP = player.getExperience();
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
        GlobalData.currentSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void returnToMainNext ()
    {
        SceneManager.LoadScene(0);
        LevelIsNext = false;
    }
}

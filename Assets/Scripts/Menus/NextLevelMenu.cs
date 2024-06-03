using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{

    public static bool LevelIsNext = false;
    public GameObject levelMenuUI;

    private bool addXP = false;

    AudioManager audioManager;

    // Update is called once per frame
    void Update () {
 
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Item").Length == 0)
        {
            if (!addXP)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                if (players.Length > 0)
                {
                    Player player = players[0].GetComponent<Player>();
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
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.levelCompleted);

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

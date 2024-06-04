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

        if (AllEnemiesDefeated() && AllPuzzlesCompleted())
        {
            if (!addXP)
            {
                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
                if (gameObjects.Length > 0)
                {
                    Player player1 = gameObjects[0].GetComponent<Player>();
                    player1.addExperience(2);
                    GlobalData.playerMaxHealth = player1.maxHealth;
                    GlobalData.playerMaxMana = player1.maxMana;
                    GlobalData.playerXP = player1.getExperience();
                }
                addXP = true;
            }

            Next();
        }

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

    private bool AllEnemiesDefeated()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    private bool AllPuzzlesCompleted()
    {
        PuzzleItem[] puzzles = FindObjectsOfType<PuzzleItem>();
        foreach (PuzzleItem puzzle in puzzles)
        {
            if (!puzzle.IsCompleted)
            {
                return false;
            }
        }
        return true;
    }

    void Next()
    {
        levelMenuUI.SetActive(true);
        LevelIsNext = true;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.levelCompleted);

    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        LevelIsNext = false;
    }

    public void returnToMainNext()
    {
        SceneManager.LoadScene(0);
        LevelIsNext = false;
    }
}

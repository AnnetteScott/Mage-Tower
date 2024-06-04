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
    void Update()
    {
        if (AllEnemiesDefeated() && AllPuzzlesCompleted())
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

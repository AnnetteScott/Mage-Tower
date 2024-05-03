using System;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }

    private bool[] puzzlesSolved; // Track completion status of individual puzzles

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

        // Initialize puzzlesSolved array based on the number of puzzles per level
        puzzlesSolved = new bool[10]; // Adjusted the size
    }

    public void SolvePuzzle(int puzzleIndex)
    {
        if (puzzleIndex >= 0 && puzzleIndex < puzzlesSolved.Length)
        {
            puzzlesSolved[puzzleIndex] = true;
            Debug.Log("Puzzle " + puzzleIndex + " solved.");

            // Example: giving coins based on the puzzle completed
            RewardSystem.Instance.GiveReward(RewardSystem.RewardType.Coins);

            // Example: Load the next level if all puzzles are solved
            if (AllPuzzlesSolved())
            {
                LevelManager.Instance.LoadNextLevel();
            }
        }
        else
        {
            Debug.LogError("Invalid puzzle index.");
        }
    }

    public bool IsPuzzleSolved(int puzzleIndex)
    {
        if (puzzleIndex >= 0 && puzzleIndex < puzzlesSolved.Length)
        {
            return puzzlesSolved[puzzleIndex];
        }
        else
        {
            Debug.LogError("Invalid puzzle index.");
            return false;
        }
    }

    public bool AllPuzzlesSolved()
    {
        foreach (bool puzzleSolved in puzzlesSolved)
        {
            if (!puzzleSolved)
            {
                return false;
            }
        }
        return true;
    }

    internal void SolvePuzzle()
    {
        throw new NotImplementedException();
    }
}

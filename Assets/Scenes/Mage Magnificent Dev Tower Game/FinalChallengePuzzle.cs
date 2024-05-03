using UnityEngine;

public class FinalChallengePuzzle : MonoBehaviour
{
    public bool finalChallengeCompleted = false; // Tracks if the final challenge is completed

    void Start()
    {
        // Initially, the final challenge is not completed
        finalChallengeCompleted = false;
    }

    public void CompleteFinalChallenge()
    {
        // The final challenge is completed
        finalChallengeCompleted = true;
        Debug.Log("Final Challenge Completed!");
        // Assuming the final challenge is the last puzzle (e.g., puzzleIndex 9 for a 0-based index)
        PuzzleManager.Instance.SolvePuzzle(9);
    }
}

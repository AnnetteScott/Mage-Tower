using UnityEngine;

public class IceBreakPuzzle : MonoBehaviour
{
    public bool iceBroken = false; // Tracks if the ice is broken

    void Start()
    {
        // Initially, the ice is not broken
        iceBroken = false;
    }

    public void BreakIce()
    {
        // The ice is broken
        iceBroken = true;
        Debug.Log("Ice Broken!");
        // Optionally, trigger the next level or clear the path
        PuzzleManager.Instance.SolvePuzzle();
    }
}

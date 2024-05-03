using UnityEngine;

public class SymbolMatchPuzzle : MonoBehaviour
{
    public bool symbolsMatched = false; // Tracks if the symbols are matched correctly

    void Start()
    {
        // Initially, the symbols are not matched
        symbolsMatched = false;
    }

    public void MatchSymbols()
    {
        // The symbols are matched correctly
        symbolsMatched = true;
        Debug.Log("Symbols Matched!");
        // Optionally, trigger the next level or open a gate
        PuzzleManager.Instance.SolvePuzzle();
    }
}
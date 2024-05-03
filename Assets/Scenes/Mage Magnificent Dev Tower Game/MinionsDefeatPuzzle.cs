using UnityEngine;

public class MinionDefeatPuzzle : MonoBehaviour
{
    public bool minionsDefeated = false; // Tracks if the minions are defeated in the correct order

    void Start()
    {
        // Initially, the minions are not defeated
        minionsDefeated = false;
    }

    public void DefeatMinions()
    {
        // The minions are defeated in the correct order
        minionsDefeated = true;
        Debug.Log("Minions Defeated!");
        // Optionally, trigger the next level or open a gate
        PuzzleManager.Instance.SolvePuzzle();
    }
}

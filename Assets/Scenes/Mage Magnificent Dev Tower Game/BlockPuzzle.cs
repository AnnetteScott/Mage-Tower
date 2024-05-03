using UnityEngine;

public class BlockPuzzle : MonoBehaviour
{
    public bool isBlockPushed = false; // Tracks if the block is pushed into place

    void Start()
    {
        // Initially, the block is not pushed
        isBlockPushed = false;
    }

    public void PushBlock()
    {
        // The block is pushed into place
        isBlockPushed = true;
        Debug.Log("Block Pushed!");
        // Optionally, trigger the next level or open a passage
        PuzzleManager.Instance.SolvePuzzle();
    }
}
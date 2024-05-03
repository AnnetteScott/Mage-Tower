using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    public bool isUnlocked = false; // Tracks if the door is unlocked

    void Start()
    {
        // Initially, the door is locked
        isUnlocked = false;
    }

    public void UnlockDoor()
    {
        // The door is unlocked
        isUnlocked = true;
        Debug.Log("Door Unlocked!");
        // Optionally, trigger the next level or open the door
        PuzzleManager.Instance.SolvePuzzle();
    }
}
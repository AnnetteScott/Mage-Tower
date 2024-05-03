using UnityEngine;

public class MapReassemblyPuzzle : MonoBehaviour
{
    public bool mapReassembled = false; // Tracks if the map is reassembled correctly

    void Start()
    {
        // Initially, the map is not reassembled
        mapReassembled = false;
    }

    public void ReassembleMap()
    {
        // The map is reassembled correctly
        mapReassembled = true;
        Debug.Log("Map Reassembled!");
        // Optionally, trigger the next level or reveal the exit
        PuzzleManager.Instance.SolvePuzzle();
    }
}

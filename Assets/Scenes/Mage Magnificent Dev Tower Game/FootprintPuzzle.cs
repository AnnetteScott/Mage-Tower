using UnityEngine;

public class FootprintPuzzle : MonoBehaviour
{
    public bool footprintsFollowed = false; // Tracks if the footprints are followed correctly

    void Start()
    {
        // Initially, the footprints are not followed
        footprintsFollowed = false;
    }

    public void FollowFootprints()
    {
        // The footprints are followed correctly
        footprintsFollowed = true;
        Debug.Log("Footprints Followed!");
        // Optionally, trigger the next level or reveal a hidden treasure
        PuzzleManager.Instance.SolvePuzzle();
    }
}
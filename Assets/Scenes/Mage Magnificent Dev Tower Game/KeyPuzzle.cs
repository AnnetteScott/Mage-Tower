using UnityEngine;

public class KeyPuzzle : MonoBehaviour
{
    public bool isFound = false; // Tracks if the key has been found

    void Start()
    {
        // Initially, the key is not found
        isFound = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player has interacted with the key
        if (other.CompareTag("Player") && !isFound)
        {
            // The key is found
            isFound = true;
            Debug.Log("Key Found!");
            // Optionally, trigger the next level or unlock a door
            PuzzleManager.Instance.SolvePuzzle();
        }
    }
}
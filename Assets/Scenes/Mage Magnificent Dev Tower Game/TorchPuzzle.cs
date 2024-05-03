using UnityEngine;

public class TorchPuzzle : MonoBehaviour
{
    public bool isLit = false; // Tracks if the torch is lit

    void Start()
    {
        // Initially, the torch is not lit
        isLit = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player has interacted with the torch
        if (other.CompareTag("Player") && !isLit)
        {
            // The torch is lit
            isLit = true;
            Debug.Log("Torch Lit!");
            // Optionally, trigger the next level or unlock a door
            PuzzleManager.Instance.SolvePuzzle();
        }
    }
}
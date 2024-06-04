using UnityEngine;

public class IceBlock : MonoBehaviour
{
    public static int totalIceBlocks = 0; // Total number of ice blocks in the scene
    public static int destroyedIceBlocks = 0; // Number of ice blocks destroyed
    private bool playerIsTouching = false; // Flag to track if the player is touching the ice block

    private void Start()
    {
        // Increment the total count of ice blocks when one is instantiated
        totalIceBlocks++;
    }

    private void OnDestroy()
    {
        // Decrement the total count of ice blocks when one is destroyed
        totalIceBlocks--;

        // Check if all ice blocks have been destroyed
        if (totalIceBlocks == 0)
        {
            AwardXP();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Set flag to true when the player starts touching the ice block
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Reset flag when the player stops touching the ice block
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsTouching = false;
        }
    }

    private void Update()
    {
        // Check if the player is touching the ice block and the "I" key is pressed
        if (playerIsTouching && Input.GetKeyDown(KeyCode.I))
        {
            Destroy(gameObject); // Destroy the ice block
        }
    }

    private void AwardXP()
    {
        // Find the player object in the scene
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Player player = playerObject.GetComponent<Player>();
            if (player != null)
            {
                player.addExperience(40); // Reward the player with 40 XP
            }
        }
    }
}

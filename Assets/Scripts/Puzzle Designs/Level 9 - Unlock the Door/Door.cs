using UnityEngine;

public class Door : MonoBehaviour
{
    public int xpReward = 50; // Reward for unlocking the door
    private bool isNearDoor = false;
    private Player player; // Reference to the Player object

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isNearDoor = true;
            player = collision.gameObject.GetComponent<Player>(); // Get the Player component

            if (player == null)
            {
                Debug.LogError("Player component not found on the collided object.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isNearDoor = false;
            player = null; // Clear the player reference when the player leaves the trigger area
        }
    }

    private void Update()
    {
        if (isNearDoor && player != null && Input.GetKeyDown(KeyCode.U)) // Check if the player is near the door and has pressed the U key
        {
            Debug.Log("Attempting to unlock the door.");
            if (player.HasKey) // Check if the player has the key
            {
                UnlockDoor(); // Call the UnlockDoor method if the player has the key
            }
            else
            {
                Debug.LogWarning("Player does not have the key.");
            }
        }
    }

    private void UnlockDoor()
    {
        if (player != null && player.HasKey) // Ensure the player has the key before proceeding
        {
            Debug.Log("Door unlocked.");

            // Reward the player with XP before destroying the door
            player.GetComponent<Player>().addExperience(xpReward);
            Debug.Log("XP awarded: " + xpReward);

            // Destroy the door after rewarding the player
            Destroy(gameObject);
            Debug.Log("Door destroyed.");
        }
        else
        {
            Debug.LogWarning("Player does not have the key or player reference is null.");
        }
    }
}

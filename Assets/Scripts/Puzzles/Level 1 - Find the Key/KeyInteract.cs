using UnityEngine;

public class KeyInteract : MonoBehaviour
{
    public int xpReward = 10; // Experience points rewarded upon interaction
    private bool isPlayerNearby = false; // Flag to check if the player is within the trigger area

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is tagged as "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the collided object is tagged as "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    void Update()
    {
        // Check if the 'K' key is pressed and the player is nearby
        if (Input.GetKeyDown(KeyCode.K) && isPlayerNearby)
        {
            // Perform the interaction
            Interact();
        }
    }

    void Interact()
    {
        // Assuming the player component is attached to the player GameObject
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Reward the player with XP
            player.GetComponent<Player>().addExperience(xpReward);

            // Remove the key from the scene
            Destroy(gameObject);
        }
    }
}

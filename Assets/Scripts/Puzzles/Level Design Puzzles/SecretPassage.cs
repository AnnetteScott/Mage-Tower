using UnityEngine;

public class SecretPassage : MonoBehaviour
{
    private bool isSolved = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isSolved)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null && player.carriedBlock != null)
            {
                // Remove the block and the secret passage
                Destroy(player.carriedBlock);
                Destroy(gameObject);
                isSolved = true;
                player.addExperience(30); // Reward the player with 30 XP
                Debug.Log("Secret passage solved, block and passage destroyed, 30 XP rewarded");
            }
        }
    }
}
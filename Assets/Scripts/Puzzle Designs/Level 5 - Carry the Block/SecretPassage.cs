using UnityEngine;

public class SecretPassage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            GameObject block = collision.gameObject;
            Destroy(block); // Remove the block from the game scene

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.GetComponent<Player>().addExperience(30); // Reward the player with 30 XP
            }
        }
    }
}

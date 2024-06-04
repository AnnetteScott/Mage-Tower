using UnityEngine;

public class SecretPassage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MovableBox"))
        {
            // Check if the block is fully in the target area
            if (Mathf.Abs(collision.transform.position.x - transform.position.x) < 0.2f &&
                Mathf.Abs(collision.transform.position.y - transform.position.y) < 0.2f)
            {
                // Remove the block and the secret passage
                Destroy(collision.gameObject);
                Destroy(gameObject);
                // Reward the player with XP
                FindObjectOfType<Player>().addExperience(30);
            }
        }
    }
}

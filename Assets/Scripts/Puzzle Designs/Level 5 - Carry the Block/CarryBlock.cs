using UnityEngine;

public class CarryBlock : MonoBehaviour
{
    public Transform blockTransform; // The block's transform component
    public Transform secretPassageEnd; // The end of the secret passage
    public int xpReward = 30; // XP reward for completing the level

    private bool isHoldingBlock = false; // Track if the player is holding the block

    private void Update()
    {
        // Pickup/Drop block logic
        if (Input.GetKeyDown(KeyCode.B))
        {
            isHoldingBlock = !isHoldingBlock;
            if (isHoldingBlock)
            {
                // Move the block with the player
                blockTransform.position = transform.position;
            }
            else
            {
                // Reset the block's position
                blockTransform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
        }

        // Check if the block has reached the secret passage
        if (isHoldingBlock && IsAtSecretPassage())
        {
            // Award XP and disable the block
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.GetComponent<Player>().addExperience(xpReward);
                Debug.Log("XP awarded: " + xpReward);
                Destroy(gameObject); // Remove the block from the scene
            }
        }
    }

    private bool IsAtSecretPassage()
    {
        // Implement logic to check if the block is at the secret passage
        // This could involve comparing the block's Y position with the secret passage's Y position
        // For simplicity, this example assumes the block reaches the secret passage when its Y position matches
        return Mathf.Abs(blockTransform.position.y - secretPassageEnd.position.y) < 0.1f;
    }
}

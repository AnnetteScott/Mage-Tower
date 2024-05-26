using UnityEngine;

public class PlayerPickupBlock : MonoBehaviour
{
    public KeyCode pickupKey = KeyCode.B; // Key to pick up the block
    private bool isCarryingBlock = false; // Track if the player is carrying the block
    public Transform blockPrefab; // Prefab of the block to instantiate
    public Transform secretPassage; // The secret passage transform

    void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            PickUpBlock();
        }
    }

    void PickUpBlock()
    {
        if (!isCarryingBlock)
        {
            // Instantiate the block at the player's position
            Instantiate(blockPrefab, transform.position, Quaternion.identity);
            isCarryingBlock = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == secretPassage)
        {
            // The block reached the secret passage
            EndPuzzle();
        }
    }

    void EndPuzzle()
    {
        // Remove the block from the scene
        Destroy(gameObject);

        // Reward the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<Player>().addExperience(30);
            Debug.Log("XP awarded: 30");
        }
    }
}

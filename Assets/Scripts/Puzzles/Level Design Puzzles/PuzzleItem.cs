using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public enum PuzzleType { Key, Block, Ice, CryptDoor }
    public PuzzleType puzzleType;
    public GameObject linkedObject; // Object that gets activated/deactivated or disappears
    public int rewardXP;
    public string requiredItem; // For puzzles that require an item (e.g., key for the crypt door)

    private bool isSolved = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isSolved)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null && player.AreAllEnemiesDefeated())
            {
                Debug.Log("Player and enemies defeated detected");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("E key pressed for puzzle interaction");
                    switch (puzzleType)
                    {
                        case PuzzleType.Key:
                            Debug.Log("Interacting with Key");
                            player.PickUpKey();
                            isSolved = true;
                            player.addExperience(rewardXP);
                            Destroy(gameObject);
                            break;
                        case PuzzleType.Block:
                            Debug.Log("Interacting with Block");
                            player.CarryBlock(gameObject);
                            break;
                        case PuzzleType.Ice:
                            Debug.Log("Interacting with Ice");
                            player.BreakIce(gameObject);
                            isSolved = true;
                            player.addExperience(rewardXP);
                            Destroy(gameObject);
                            break;
                        case PuzzleType.CryptDoor:
                            Debug.Log("Interacting with CryptDoor");
                            if (player.HasItem(requiredItem))
                            {
                                Debug.Log("Player has required item: " + requiredItem);
                                player.UseItem(requiredItem);
                                isSolved = true;
                                player.addExperience(rewardXP);
                                Destroy(linkedObject);
                                Destroy(gameObject);
                            }
                            else
                            {
                                Debug.Log("Player does not have required item: " + requiredItem);
                            }
                            break;
                    }
                }
            }
        }
    }
}

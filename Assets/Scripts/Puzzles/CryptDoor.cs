using UnityEngine;

public class CryptDoor : MonoBehaviour
{
    public GameObject door;
    public int xpReward = 50;

    public void Unlock()
    {
        // Reward XP
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player>().addExperience(xpReward);
        // Mark puzzle as completed
        GlobalData.level9PuzzleCompleted = true;
        // Open the door (you can add animation or other effects here)
        Destroy(door);
        // Proceed to the next level
        FindObjectOfType<LevelMenu>().NextLevelButton();
    }
}

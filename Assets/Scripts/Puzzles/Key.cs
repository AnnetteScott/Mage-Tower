using UnityEngine;

public class Key : MonoBehaviour
{
    private bool canPickUp = false;

    private void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            GlobalData.inventory.Add("Key");
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().addExperience(10);
            GlobalData.level1PuzzleCompleted = true; // Mark the puzzle as completed
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            canPickUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canPickUp = false;
        }
    }
}

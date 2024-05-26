using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    private bool isCarried = false;
    private GameObject player;

    void Update()
    {
        if (isCarried)
        {
            // Follow the player's position
            transform.position = player.transform.position + new Vector3(1f, 0, 0); // Adjust offset as needed
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }

    public void PickUp()
    {
        isCarried = true;
        GetComponent<Collider2D>().enabled = false; // Disable the collider to avoid interference
    }

    public void Drop()
    {
        isCarried = false;
        GetComponent<Collider2D>().enabled = true; // Re-enable the collider
    }
}

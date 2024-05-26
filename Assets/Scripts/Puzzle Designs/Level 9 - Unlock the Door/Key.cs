using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isNearKey = false;
    private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isNearKey = true;
            player = collision.gameObject.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isNearKey = false;
            player = null;
        }
    }

    private void Update()
    {
        if (isNearKey && player != null && Input.GetKeyDown(KeyCode.L))
        {
            player.PickUpKey();
            Destroy(gameObject);
        }
    }
}

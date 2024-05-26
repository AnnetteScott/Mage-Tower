using UnityEngine;

public class IceBlockController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && HammerController.IsPickedUp)
        {
            Destroy(gameObject);
        }
    }
}

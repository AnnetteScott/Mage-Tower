using UnityEngine;

public class HammerController : MonoBehaviour
{
    public static bool IsPickedUp = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !IsPickedUp)
        {
            IsPickedUp = true;
            Destroy(gameObject);
        }
    }
}

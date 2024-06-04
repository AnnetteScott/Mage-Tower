using System.Collections;
using UnityEngine;

public class TorchLighter : MonoBehaviour
{
    public int xpReward = 20; // Reward for lighting up all torches
    private bool isLit = false; // Tracks if the torch is lit
    private static int totalLitTorchCount = 0; // Counts total lit torches

    private void Start()
    {
        // Initially, make the torch partially visible
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.1f); // Partially visible
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitForKeyPress());
        }
    }

    private IEnumerator WaitForKeyPress()
    {
        // Wait for the 'T' key to be pressed
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.T));

        // Once the 'T' key is pressed, light up the torch
        LightTorch();
    }

    void LightTorch()
    {
        if (!isLit)
        {
            isLit = true;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f); // Fully visible

            // Increment the total lit torch count
            totalLitTorchCount++;
            Debug.Log("Total lit torches: " + totalLitTorchCount);

            // Check if all torches are lit
            if (totalLitTorchCount == 5)
            {
                // All torches are lit, reward the player
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.GetComponent<Player>().addExperience(xpReward);
                    Debug.Log("XP awarded: " + xpReward);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBullet : Bullet
{
    public int maxBounces = 3;  // Limit the number of bounces
    private int bounceCount = 0;

    private Rigidbody2D rb;

    private new void Start()
    {
        startPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    public new void Setup(Vector3 end)
    {
        base.Setup(end);
        Vector2 direction = ((Vector2)end - (Vector2)transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    // Override the OnTriggerStay2D method to prevent the base implementation from being called
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            bounceCount++;
            if (bounceCount >= maxBounces)
            {
                Destroy(gameObject);
            }
            else
            {
                // Reflect the bullet's velocity upon collision
                Vector2 normal = (transform.position - collision.transform.position).normalized;
                rb.velocity = Vector2.Reflect(rb.velocity, normal);
            }
        }
    }
}
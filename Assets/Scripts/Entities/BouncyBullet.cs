using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBullet : Bullet
{
    public int maxBounces = 3;  // Limit the number of bounces
    private int bounceCount = 0;

    private Rigidbody2D rb;

    private void Start()
    {
        startPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Setup(Vector3 end)
    {
        base.Setup(end);
        Vector2 direction = (end - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
                Vector2 normal = collision.contacts[0].normal;
                rb.velocity = Vector2.Reflect(rb.velocity, normal);
            }
        }
    }
}
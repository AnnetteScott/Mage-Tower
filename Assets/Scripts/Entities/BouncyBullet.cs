using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBullet : Bullet
{
    public int maxBounces = 3;  // Limit the number of bounces
    private int bounceCount = 0;

    protected override void Start()
    {
        base.Start();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
    }

    public override void Setup(Vector3 end)
    {
        base.Setup(end);

        // Ignore collisions with the player
        Collider2D[] playerColliders = GameObject.FindGameObjectWithTag("Player").GetComponents<Collider2D>();
        foreach (Collider2D playerCollider in playerColliders)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerCollider);
        }
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
            /*else
            {
                // Reflect the bullet's velocity upon collision
                Vector2 normal = collision.contacts[0].normal; // Get the collision normal from the first contact point
                rb.velocity = Vector2.Reflect(rb.velocity, normal);
            } */
        }
    }
}
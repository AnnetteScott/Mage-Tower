using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    public float moveSpeed = 20f;
    public int damage = 4;
    public int manaCost = 2;

    protected Rigidbody2D rb;
    protected Vector2 startPoint;
    protected Vector2 endPoint;
    
    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        startPoint = transform.position;
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0; // Ensure the bullet is not affected by gravity
        }
    }

    public virtual void Setup(Vector3 end) 
    {
        endPoint = end;
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (endPoint - (Vector2)transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            Debug.LogError("Rigidbody2D component is missing on the bullet.");
        }
    }

    public float getDamage()
    {
        float damageModifier = 1;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            damageModifier = players[0].GetComponent<Player>().power;
        }

        return damage * damageModifier;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}

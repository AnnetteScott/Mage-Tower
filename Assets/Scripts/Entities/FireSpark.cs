using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpark : Bullet
{
    private float duration = 3f;
    private float fstimer;
    private float hitTimer = 0;
    private float hitTimeout = 1f;

    private new void Start()
    {
        startPoint = transform.position;
        // Fire Spark doesn't move, so no need to set up any velocity or direction.

        //Check if the Fire Spark is colliding with the ground
        if (IsOnGround()) {
            Destroy(gameObject);
        } else {
            fstimer = duration; //initialise the timer
        }
    }

    private void Update() {
        fstimer -= Time.deltaTime; //decrease the timer
        if (fstimer <= 0) {
            Destroy(gameObject);
        }

        if (hitTimer > 0)
        {
            hitTimer -= Time.deltaTime;
        }
    }

    public new void Setup(Vector3 position)
    {
        transform.position = position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hitTimer <= 0f)
        {
            collision.gameObject.GetComponent<Enemy>().takeDamage(damage);
            hitTimer = hitTimeout; //Reset the hit timer
        }

        if (collision.gameObject.CompareTag("Player") && hitTimer <= 0f)
        {
            collision.gameObject.GetComponent<Player>().takeDamage(damage);
            hitTimer = hitTimeout; //Reset the hit timer
        }
    }

    private bool IsOnGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        foreach (Collider2D collider in colliders) {
            if(collider.CompareTag("Ground")) {
                return true;
            }
        }
        return false;
    }
}

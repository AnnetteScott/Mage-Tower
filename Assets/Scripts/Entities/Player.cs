using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public InputAction move;
    public InputAction run;
    public float jumpHeight;
    public float walkingSpeed;
    public float runningSpeed;
    private Rigidbody2D rigidBody;
    private bool onGround;

    void Start()
    {
        init();
        onGround = true;
        move.Enable();
        run.Enable();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;

    }

    void Update()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        float speed = walkingSpeed;
        if (run.IsPressed())
        {
            speed = runningSpeed;
        }


        if (move.IsPressed())
        {
            Vector2 moveValue = move.ReadValue<Vector2>();

            if (onGround && moveValue.y != 0.0f)
            {
                onGround = false;
                rigidBody.velocity = new Vector2(moveValue.x * speed, jumpHeight);
            }
            else
            {
                rigidBody.velocity = new Vector2(moveValue.x * speed, rigidBody.velocity.y);
            }

            if (moveValue.x != 0.0f)
            {
                Vector3 localScale = transform.localScale;
                localScale.x = moveValue.x < 0.0f ? -1 : 1;
                transform.localScale = localScale;
            }

        }
        else if (onGround)
        {
            rigidBody.velocity = new Vector2(0, 0);
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
}

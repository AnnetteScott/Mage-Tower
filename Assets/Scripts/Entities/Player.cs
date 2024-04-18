using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public InputAction move;
    public InputAction run;
    public InputAction mouse;
    public float jumpHeight;
    public float walkingSpeed;
    public float runningSpeed;
    private Rigidbody2D rigidBody;
    private bool onGround = true;
    private bool jumped = false;
    private float hittingTimer = 0;
    public float hitTimeOut = 0.5f;
    public Animator animator;

    void Start()
    {
        init();
        move.Enable();
        mouse.Enable();
        run.Enable();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;

    }

    void Update()
    {
        movePlayer();
        attack();
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
                jumped = true;

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
        else if(jumped)
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }

    private void attack()
    {
        if (mouse.IsPressed() && hittingTimer <= 0)
        {
            hittingTimer = hitTimeOut;
            animator.Play("Staff Hit", -1, 0f);
            animator.SetBool("isHitting", true);
            animator.SetBool("isHitting", false);
            //hit
        }

        if (hittingTimer > 0)
        {
            hittingTimer -= Time.deltaTime;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hittingTimer > 0.02f)
        {
            GameObject enemy = collision.gameObject;
            enemy.GetComponent<Enemy>().takeDamage(damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            jumped = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}

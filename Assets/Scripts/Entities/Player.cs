using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : Entity
{
    public Slider manaSlider;
    public Slider xpSlider;
    public Animator animator;
    public Text healthText;
    public Text manaText;
    public Text xpText;
    public Text levelText;
    public Collider2D feetCollider;
    public Collider2D bodyCollider;
    public float jumpHeight;
    public float walkingSpeed;
    public float runningSpeed;
    public float maxMana;
    public float hitTimeOut = 0.5f;
    public InputAction move;
    public InputAction run;
    public InputAction mouse;

    private float mana;
    private int experience;
    private Rigidbody2D rigidBody;
    private bool onGround = false;
    private bool hitting = false;
    private float hittingTimer = 0;

    void Start()
    {
        setHealthToMax();
        move.Enable();
        mouse.Enable();
        run.Enable();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
        mana = maxMana;
        manaText.text = mana.ToString() + "/" + maxMana;
    }

    void FixedUpdate()
    {
        movePlayer();
        attack();
        healthText.text = getHealth() + "/" + maxHealth;
    }

    /// <summary>
    /// Apply movement to the player
    /// </summary>
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

            //Player is jumping
            if (onGround && moveValue.y != 0.0f)
            {
                rigidBody.velocity = new Vector2(moveValue.x * speed, jumpHeight);
            }
            else
            {
                rigidBody.velocity = new Vector2(moveValue.x * speed, rigidBody.velocity.y);
            }

            //Player mvoing and may be facing a different direction
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

    /// <summary>
    /// Swing the staff
    /// </summary>
    private void attack()
    {
        if (mouse.IsPressed() && hittingTimer <= 0)
        {
            hitting = true;
            hittingTimer = hitTimeOut;
            animator.Play("Staff Hit", -1, 0f);
            animator.SetBool("isHitting", true);
            animator.SetBool("isHitting", false);
        }

        if (hittingTimer > 0)
        {
            hittingTimer -= Time.deltaTime;
        }
        else
        {
            hitting = false;
        }
    }

    /// <summary>
    /// Use mana if the player has enough
    /// </summary>
    /// <param name="manaUsed"></param>
    /// <returns>true if the mana was used, false otherwise</returns>
    public Boolean useMana(float manaUsed)
    {
        if(mana - manaUsed > 0) 
        {
            mana -= manaUsed;
            manaSlider.value = mana / maxMana;
            manaText.text = mana.ToString() + "/" + maxMana;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Calculates the level of the entity
    /// </summary>
    /// <returns>int of the entities current level</returns>
    public int getLevel()
    {
        return Mathf.Max(Mathf.FloorToInt(Mathf.Sqrt(this.experience)), 1);
    }

    /// <summary>
    /// Add experience to the total
    /// </summary>
    /// <param name="experience"></param>
    public void addExperience(int experience)
    {
        int currentLevel = getLevel();
        this.experience += Mathf.Abs(experience);
        int newLevel = getLevel();

        if (currentLevel != newLevel)
        {
            levelUp();
        }
    }

    /// <summary>
    /// Level up the entity
    /// </summary>
    private void levelUp()
    {
        this.maxHealth += 20;
        setHealthToMax();
        //Trigger vfx for leveling up
    }

    /// <summary>
    /// If the staff hits an enemy and the player has swang, do damage to the enemy
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hitting)
        {
            hitting = false;
            GameObject enemy = collision.gameObject;
            float enemyHealth = enemy.GetComponent<Enemy>().takeDamage(damage);
        }
    }

    /// <summary>
    /// Check if the player is on the ground
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && feetCollider.IsTouching(collision.collider))
        {
            onGround = true;
        }
    }

    /// <summary>
    /// Check if the player is no longer on the ground
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}

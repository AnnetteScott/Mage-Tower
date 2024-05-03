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
    public SpriteRenderer playerSprite;
    public SpriteRenderer staffSprite;
    public float jumpHeight;
    public float walkingSpeed;
    public float runningSpeed;
    public float maxMana;
    public float hitTimeOut = 0.5f;
    public InputAction move;
    public InputAction dash;
    public InputAction run;
    public InputAction mouse;

    private float mana;
    private int experience;
    private Rigidbody2D rigidBody;
    private bool onGround = false;
    private bool hitting = false;
    private float hittingTimer = 0;
    private float dashingTimer = 0;
    private float dashingTimeout = 0.2f;
    private float dashingSpeed = 40f;
    private float dashingManaUse = 3;


    public bool isFlipped = false;

    void Start()
    {
        setHealthToMax();
        move.Enable();
        mouse.Enable();
        run.Enable();
        dash.Enable();
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

        if (dash.IsPressed() && dashingTimer == 0 && mana > dashingManaUse)
        {
            dashingTimer = dashingTimeout;
            rigidBody.velocity = new Vector2(isFlipped ? -dashingSpeed : dashingSpeed, 0);
            useMana(dashingManaUse);
        }
        else if(dashingTimer > 0)
        {
            rigidBody.velocity = new Vector2(isFlipped ? -dashingSpeed : dashingSpeed, 0);
        }
        else if (move.IsPressed())
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

        }
        else if (onGround)
        {
            rigidBody.velocity = new Vector2(0, 0);
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }

        if(dashingTimer > 0)
        {
            dashingTimer -= Time.deltaTime;
        }
        else
        {
            dashingTimer = 0;
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
        if(mana - manaUsed >= 0) 
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

        levelText.text = getLevel().ToString();
        float xpneeded = Mathf.Pow(getLevel() + 1, 2);
        xpText.text = experience.ToString() + "/" + xpneeded;
        xpSlider.value = experience / xpneeded;
    }

    /// <summary>
    /// Level up the entity
    /// </summary>
    private void levelUp()
    {
        this.maxHealth += 5;
        setHealthToMax();
        this.maxMana += 5;
        mana = maxMana;
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
            if (enemyHealth <= 0)
            {
                addExperience(3);
            }
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

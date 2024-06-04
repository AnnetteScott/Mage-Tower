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
    public float jumpHeight;
    public float walkingSpeed;
    public float runningSpeed;
    public float maxMana;
    public float hitTimeOut = 0.5f;
    public InputAction move;
    public InputAction dash;
    public InputAction run;
    public InputAction mouse;
    public KeyCode pickUpKeyCode = KeyCode.B;
    public KeyCode interactKeyCode = KeyCode.E;
    public KeyCode useKeyCode = KeyCode.Q;
    public bool hasKey = false;

    private float mana;
    private int experience;
    private Rigidbody2D rigidBody;
    private bool onGround = false;
    private bool hitting = false;
    private GameObject carriedBlock = null;
    private GameObject playerNearbyBlock = null;
    public float pushPower = 2.0f;
    private GameObject playerNearbyPuzzleItem;

    private float hittingTimer = 0;
    private float dashingTimer = 0;
    private float dashingTimeout = 1f;
    private float dashingTimelimit = 0.1f;
    private float dashingSpeed = 20f;
    private float dashingManaUse = 2;
    private bool isDashingRight = true;

    public bool isFlipped = false;

    void Start()
    {
        if (GlobalData.playerMaxHealth == 0)
        {
            GlobalData.playerMaxHealth = maxHealth;
            GlobalData.playerMaxMana = maxMana;
            GlobalData.playerXP = 0;
        }

        maxHealth = GlobalData.playerMaxHealth;
        maxMana = GlobalData.playerMaxMana;
        experience = GlobalData.playerXP;

        setHealthToMax();
        move.Enable();
        mouse.Enable();
        run.Enable();
        dash.Enable();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
        mana = maxMana;
        updateGUI();

        power = GlobalData.playerPower;
        armour = GlobalData.playerArmour;
    }

    void Update()
    {
        movePlayer();
        attack();
        updateGUI();

        if (Input.GetKeyDown(pickUpKeyCode))
        {
            if (carriedBlock == null && playerNearbyBlock != null)
            {
                // Pick up the block
                playerNearbyBlock.GetComponent<BlockInteraction>().PickUp();
                carriedBlock = playerNearbyBlock;
            }
            else if (carriedBlock != null)
            {
                // Drop the block
                carriedBlock.GetComponent<BlockInteraction>().Drop();
                carriedBlock = null;
            }
        }

        if (Input.GetKeyDown(interactKeyCode))
        {
            if (playerNearbyPuzzleItem != null)
            {
                Debug.Log("Interacting with: " + playerNearbyPuzzleItem.name);
                playerNearbyPuzzleItem.GetComponent<PuzzleItem>().Interact();
            }
        }

        if (Input.GetKeyDown(useKeyCode))
        {
            UseKey();
        }
    }

    private void UseKey()
    {
        if (hasKey && playerNearbyPuzzleItem != null && playerNearbyPuzzleItem.CompareTag("CryptDoor"))
        {
            playerNearbyPuzzleItem.GetComponent<PuzzleItem>().Unlock();
            hasKey = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            GlobalData.inventory.Add(collision.gameObject.name.Replace("(Clone)", ""));
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Block"))
        {
            playerNearbyBlock = collision.gameObject;
        }

        if (collision.gameObject.CompareTag("PuzzleItem"))
        {
            playerNearbyPuzzleItem = collision.gameObject;
            Debug.Log("Nearby puzzle item detected: " + playerNearbyPuzzleItem.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PuzzleItem"))
        {
            playerNearbyPuzzleItem = null;
            Debug.Log("Puzzle item exited: " + collision.gameObject.name);
        }
    }

    private void movePlayer()
    {
        float speed = walkingSpeed;
        if (run.IsPressed())
        {
            speed = runningSpeed;
        }

        if (dash.IsPressed() && dashingTimer == 0 && mana >= dashingManaUse)
        {
            isDashingRight = playerSprite.flipX;
            dashingTimer = dashingTimeout;
            rigidBody.velocity = new Vector2(isDashingRight ? -dashingSpeed : dashingSpeed, 0);
            useMana(dashingManaUse);
        }
        else if (dashingTimer > dashingTimeout - dashingTimelimit)
        {
            rigidBody.velocity = new Vector2(isDashingRight ? -dashingSpeed : dashingSpeed, 0);
        }
        else if (move.IsPressed())
        {
            Vector2 moveValue = move.ReadValue<Vector2>();

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

        if (dashingTimer > 0)
        {
            dashingTimer -= Time.deltaTime;
        }
        else
        {
            dashingTimer = 0;
        }
    }

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

    public Boolean useMana(float manaUsed)
    {
        if (mana - manaUsed >= 0)
        {
            mana -= manaUsed;
            return true;
        }
        return false;
    }

    public int getLevel()
    {
        return Mathf.Max(Mathf.FloorToInt(Mathf.Sqrt(this.experience)), 1);
    }

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

    private void levelUp()
    {
        this.maxHealth += 2;
        setHealthToMax();
        this.maxMana += 2;
        mana = maxMana;
    }

    public int getExperience()
    {
        return this.experience;
    }

    public void updateGUI()
    {
        levelText.text = getLevel().ToString();
        float xpneeded = Mathf.Pow(getLevel() + 1, 2);
        xpText.text = experience.ToString() + "/" + xpneeded;
        xpSlider.value = experience / xpneeded;

        manaSlider.value = mana / maxMana;
        manaText.text = mana.ToString() + "/" + maxMana;

        healthText.text = getHealth() + "/" + maxHealth;
        healthSlider.value = getHealth() / maxHealth;
    }

    public void killedEnemy()
    {
        addExperience(3);
        addHealth(2);
        mana += 2;
        if (mana > maxMana)
        {
            mana = maxMana;
        }

        updateGUI();
    }

    public void pickUpKey()
    {
        hasKey = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hitting)
        {
            hitting = false;
            GameObject enemy = collision.gameObject;
            enemy.GetComponent<Enemy>().takeDamage(damage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && feetCollider.IsTouching(collision.collider))
        {
            onGround = true;
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

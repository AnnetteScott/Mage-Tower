using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public float maxHealth;
    public int damage;
    public Slider healthSlider;
    private float health;
    private int experience;

    /// <summary>
    /// 
    /// </summary>
    public void init()
    {
        health = maxHealth;
    }


    /// <summary>
    /// Gets the health of the entity
    /// </summary>
    /// <returns>int of the entities health</returns>
    public float getHealth()
    {
        return health;
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

        if(currentLevel != newLevel)
        {
            levelUp();
        }
    }

    /// <summary>
    /// Decrease the health of the entity after taking damage
    /// </summary>
    /// <param name="damage"></param>
    /// <returns>int of the remaining health</returns>
    public float takeDamage(int damage)
    {
        health -= Mathf.Abs(damage);
        healthSlider.value = health / maxHealth;

        if (this.health <= 0)
        {
            died();
        }

        return this.health;
    }

    /// <summary>
    /// The entity has died
    /// </summary>
    public void died()
    {
        if (gameObject.transform.parent != null && gameObject.transform.parent.CompareTag("Enemy"))
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
        else
        {
            gameObject.GetComponent<Player>().healthText.text = "0/" + maxHealth;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Level up the entity
    /// </summary>
    private void levelUp()
    {
        this.maxHealth += 20;
        this.health = maxHealth;
        //Trigger vfx for leveling up
    }
}

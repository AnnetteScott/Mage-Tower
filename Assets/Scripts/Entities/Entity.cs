using UnityEngine;

public class Entity
{
    private int health;
    private int maxHealth;
    private int experience;


    public Entity()
    {
        experience = 0;
        health = 10;
        maxHealth = 10;
    }

    /// <summary>
    /// Gets the health of the entity
    /// </summary>
    /// <returns>int of the entities health</returns>
    public int getHealth()
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
        this.experience += experience;
        int newLevel = getLevel();

        if(currentLevel != newLevel)
        {
            levelUp();
        }
    }

    /// <summary>
    /// Hit with melee weapon
    /// </summary>
    public void hit()
    {
        
    }

    /// <summary>
    /// Decrease the health of the entity after taking damage
    /// </summary>
    /// <param name="damage"></param>
    public void takeDamage(int damage)
    {
        this.health -= Mathf.Abs(damage);

        if(this.health < 0)
        {
            //Entity is dead
        }
    }

    public void levelUp()
    {
        this.maxHealth += 20;
        this.health = maxHealth;
        //Trigger vfx for leveling up
    }
}

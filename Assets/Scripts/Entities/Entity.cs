using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public float maxHealth;
    public int damage;
    public float amour;
    public Slider healthSlider;
    public GameObject drop;
    private float health;


    /// <summary>
    /// 
    /// </summary>
    public void setHealthToMax()
    {
        health = maxHealth;
    }


    public void addHealth(float health)
    {
        health += Mathf.Abs(health);
        if(health > maxHealth) 
        { 
            health = maxHealth;
        }

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
    /// Decrease the health of the entity after taking damage
    /// </summary>
    /// <param name="damage"></param>
    /// <returns>int of the remaining health</returns>
    public float takeDamage(float damage)
    {
        float damageDone = damage - amour;
        if(damageDone > 0)
        {
            health -= Mathf.Abs(damageDone);
            healthSlider.value = health / maxHealth;

        }

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
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if(players.Length > 0)
            {
                players[0].GetComponent<Player>().killedEnemy();
            }
            if(drop != null)
            {
                GameObject newInstance = Instantiate(drop);
                newInstance.transform.SetParent(transform.root);
            }
            Destroy(gameObject.transform.parent.gameObject);
        }
        else
        {
            gameObject.GetComponent<Player>().healthText.text = "0/" + maxHealth;
            Destroy(gameObject);
        }
    }
}

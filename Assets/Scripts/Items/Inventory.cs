using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IPointerDownHandler
{
    public static bool GameIsPaused = false;
    public GameObject inventoryUI;
    public GameObject staffSlot;
    public GameObject armourSlot;
    public GameObject InventorySlots;
    public TextMeshProUGUI itemPower;
    public TextMeshProUGUI playerStats;
    public Image playerImage;
    public Image staffCrystal;
    private GameObject clickedItem;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        setPlayerVisuals();
    }

    // Update is called once per frame
    void Update()
    {
        //update stats for the player
        setPlayerStats();
        //Show the inventory menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameIsPaused)
            {
                Resume();
                clickedItem = null;
            }
            else
            {
                Pause();
                displayInventoryItems();
            }
        }
    }

    /// <summary>
    /// Display all the items in the inventory menu
    /// </summary>
    private void displayInventoryItems()
    {
        //Clear all items
        for (int i = 0; i < InventorySlots.transform.childCount; i++)
        {
            if (InventorySlots.transform.GetChild(i).childCount > 0)
            {
                Destroy(InventorySlots.transform.GetChild(i).GetChild(0).gameObject);
            }
        }

        if (staffSlot.transform.childCount > 0)
        {
            Destroy(staffSlot.transform.GetChild(0).gameObject);
        }

        if (armourSlot.transform.childCount > 0)
        {
            Destroy(armourSlot.transform.GetChild(0).gameObject);
        }

        staffSlot.gameObject.GetComponent<Image>().color = Color.white;
        armourSlot.gameObject.GetComponent<Image>().color = Color.white;

        //Add all items
        int index = 0;
        foreach (string name in GlobalData.inventory)
        {
            var resource = Resources.Load(name);
            GameObject newInstance = Instantiate(resource) as GameObject;
            Transform nextSlot = InventorySlots.transform.GetChild(index);
            newInstance.transform.SetParent(nextSlot.transform);
            index++;
        }

        if (GlobalData.equippedStaffItem != null)
        {
            var resource = Resources.Load(GlobalData.equippedStaffItem);
            GameObject newInstance = Instantiate(resource) as GameObject;
            newInstance.transform.SetParent(staffSlot.transform);
            string variant = newInstance.name.Replace("(Clone)", "").Replace(" Variant", "").Split(" - ")[1];
            staffCrystal.sprite = (Sprite)Resources.Load("Staff Crystal - " + variant, typeof(Sprite));
        }

        if (GlobalData.equippedArmourItem != null)
        {
            var resource = Resources.Load(GlobalData.equippedArmourItem);
            GameObject newInstance = Instantiate(resource) as GameObject;
            newInstance.transform.SetParent(armourSlot.transform);
            string variant = newInstance.name.Replace("(Clone)", "").Replace(" Variant", "").Split(" - ")[1];
            playerImage.sprite = (Sprite)Resources.Load("Player - " + variant, typeof(Sprite));
        }
    }

    /// <summary>
    /// Set the in game player character sprites to match equipped items
    /// </summary>
    private void setPlayerVisuals()
    {
        if (GlobalData.equippedStaffItem != null)
        {
            string crystalVariant = GlobalData.equippedStaffItem.Replace(" Variant", "").Split(" - ")[1];
            GameObject[] staffCrystal = GameObject.FindGameObjectsWithTag("StaffCrystal");
            staffCrystal[0].GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Staff Crystal - " + crystalVariant, typeof(Sprite));
        }

        if (GlobalData.equippedArmourItem != null)
        {
            string armourVariant = GlobalData.equippedArmourItem.Replace(" Variant", "").Split(" - ")[1];
            player.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Player - " + armourVariant, typeof(Sprite));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (clickedItem != null)
        {
            Image slotImage = clickedItem.transform.parent.gameObject.GetComponent<Image>();
            slotImage.color = Color.white;
        }

        GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
        if (gameObject.CompareTag("Item"))
        {
            clickedItem = gameObject;
            Image slotImage = clickedItem.transform.parent.gameObject.GetComponent<Image>();
            slotImage.color = Color.cyan;
            setStats();
        }
        else
        {
            itemPower.text = string.Empty;
        }
    }

    /// <summary>
    /// Display armour/weapon stats
    /// </summary>
    public void setStats()
    {
        if (clickedItem.GetComponent<Weapon>() != null)
        {
            itemPower.text = "Damage Modifier: " + clickedItem.GetComponent<Weapon>().power.ToString() + "x";
        }
        else if (clickedItem.GetComponent<Armour>() != null)
        {
            itemPower.text = "Armour: " + clickedItem.GetComponent<Armour>().armour.ToString();
        }
    }

    /// <summary>
    /// Display the player stats
    /// </summary>
    private void setPlayerStats()
    {
        playerStats.text = "Power: " + GlobalData.playerPower + "\n" + "Armour: " + GlobalData.playerArmour + "\n\n" + "Health: " + GlobalData.playerMaxHealth + "\n" + "Mana: " + GlobalData.playerMaxMana + "\n" + "Level: " + GlobalData.playerLevel + " (" + GlobalData.playerXP + " XP)";
    }

    /// <summary>
    /// Equip the clicked item
    /// </summary>
    public void equip()
    {
        if (!clickedItem)
        {
            return;
        }

        string name = clickedItem.name.Replace("(Clone)", "");
        string variant = name.Replace(" Variant", "").Split(" - ")[1];
        if (clickedItem.GetComponent<Weapon>() != null)
        {
            if (staffSlot.transform.childCount > 0)
            {
                GameObject item = staffSlot.transform.GetChild(0).gameObject;
                GlobalData.inventory.Add(item.name.Replace("(Clone)", ""));
            }

            float power = clickedItem.GetComponent<Weapon>().power;
            GlobalData.equippedStaffItem = name;
            GlobalData.playerPower = power;
            player.GetComponent<Player>().power = power;

        }
        else if (clickedItem.GetComponent<Armour>() != null)
        {
            if(armourSlot.transform.childCount > 0)
            {
                GameObject item = armourSlot.transform.GetChild(0).gameObject;
                GlobalData.inventory.Add(item.name.Replace("(Clone)", ""));
            }

            float armour = clickedItem.GetComponent<Armour>().armour;
            GlobalData.equippedArmourItem = name;
            GlobalData.playerArmour = armour;
            player.GetComponent<Player>().armour = armour;
        }
        else if (name == "Key" || name == "Hammer")
        {
            // Handle puzzle items
            GlobalData.inventory.Remove(name);
            if (name == "Key")
            {
                player.GetComponent<Player>().hasKey = true;
            }
        }

        GlobalData.inventory.Remove(name);
        displayInventoryItems();
        Image slotImage = clickedItem.transform.parent.gameObject.GetComponent<Image>();
        slotImage.color = Color.white;
        clickedItem = null;
        itemPower.text = string.Empty;
        setPlayerVisuals();
    }

    /// <summary>
    /// unequip the clicked item
    /// </summary>
    public void unequip()
    {
        if (!clickedItem)
        {
            return;
        }

        Transform nextSlot = InventorySlots.transform.GetChild(GlobalData.inventory.Count);
        clickedItem.transform.SetParent(nextSlot);
        GlobalData.inventory.Add(clickedItem.name.Replace("(Clone)", ""));
        if (clickedItem.GetComponent<Weapon>() != null)
        {
            player.GetComponent<Player>().power = 1;
            GlobalData.equippedStaffItem = null;
            GlobalData.playerPower = 1;
        }
        else if (clickedItem.GetComponent<Armour>() != null)
        {
            GlobalData.equippedArmourItem = null;
            player.GetComponent<Player>().armour = 0;
            GlobalData.playerArmour = 0;
        }
        Image slotImage = clickedItem.transform.parent.gameObject.GetComponent<Image>();
        slotImage.color = Color.white;
        displayInventoryItems();
        clickedItem = null;
        itemPower.text = string.Empty;
    }

    public void Resume()
    {
        inventoryUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        inventoryUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}

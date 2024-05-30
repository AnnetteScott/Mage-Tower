using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.UIElements.ToolbarMenu;

public class Inventory : MonoBehaviour, IPointerDownHandler
{
    public static bool GameIsPaused = false;
    public GameObject inventoryUI;
    public GameObject staffSlot;
    public GameObject amourSlot;
    public GameObject InventorySlots;
    public TextMeshProUGUI itemPower;
    public Image playerImage;
    public Image staff;
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
    /// Display all the items in the inventory meny
    /// </summary>
    private void displayInventoryItems()
    {
        //Clear all items
        for(int i = 0; i < InventorySlots.transform.childCount; i++)
        {
            if(InventorySlots.transform.GetChild(i).childCount > 0)
            {
                Destroy(InventorySlots.transform.GetChild(i).GetChild(0).gameObject);
            }
        }

        if (staffSlot.transform.childCount > 0)
        {
            Destroy(staffSlot.transform.GetChild(0).gameObject);
        }

        if (amourSlot.transform.childCount > 0)
        {
            Destroy(amourSlot.transform.GetChild(0).gameObject);
        }

        staffSlot.gameObject.GetComponent<Image>().color = Color.white;
        amourSlot.gameObject.GetComponent<Image>().color = Color.white;

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

        if(GlobalData.equippedStaffItem != null)
        {
            var resource = Resources.Load(GlobalData.equippedStaffItem);
            GameObject newInstance = Instantiate(resource) as GameObject;
            newInstance.transform.SetParent(staffSlot.transform);
            string variant = newInstance.name.Replace("(Clone)", "").Replace(" Variant", "").Split(" - ")[1];
            staff.sprite = (Sprite)Resources.Load("Staff - " + variant, typeof(Sprite));
        }

        if(GlobalData.equippedArmourItem != null)
        {
            var resource = Resources.Load(GlobalData.equippedArmourItem);
            GameObject newInstance = Instantiate(resource) as GameObject;
            newInstance.transform.SetParent(amourSlot.transform);
            string variant = newInstance.name.Replace("(Clone)", "").Replace(" Variant", "").Split(" - ")[1];
            playerImage.sprite = (Sprite)Resources.Load("Player - " + variant, typeof(Sprite));
        }
    }

    /// <summary>
    /// Set the in game player character sprites to match equipped items
    /// </summary>
    private void setPlayerVisuals()
    {
        GameObject staff = GameObject.FindGameObjectWithTag("Weapon");
        if (GlobalData.equippedStaffItem != null)
        {
            string variant = GlobalData.equippedStaffItem.Replace(" Variant", "").Split(" - ")[1];
            staff.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Staff - " + variant, typeof(Sprite));
        }
        else
        {
            staff.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Staff", typeof(Sprite));
        }

        if (GlobalData.equippedArmourItem != null)
        {
            string armourVariant = GlobalData.equippedArmourItem.Replace(" Variant", "").Split(" - ")[1];
            player.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Player - " + armourVariant, typeof(Sprite));
        }
        else
        {
            player.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Player", typeof(Sprite));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(clickedItem != null)
        {
            Image slotImage = clickedItem.transform.parent.gameObject.GetComponent<Image>();
            slotImage.color = Color.white;
        }

        GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
        if(gameObject.CompareTag("Item"))
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
            itemPower.text = "Amour: " + clickedItem.GetComponent<Armour>().armour.ToString();
        }
    }

    /// <summary>
    /// Equip the clicked item
    /// </summary>
    public void equip()
    {
        if(!clickedItem)
        {
            return;
        }

        string name = clickedItem.name.Replace("(Clone)", "");
        string variant = name.Replace(" Variant", "").Split(" - ")[1];
        if(clickedItem.GetComponent<Weapon>() != null)
        {
            if(staffSlot.transform.childCount > 0)
            {
                GameObject item = staffSlot.transform.GetChild(0).gameObject;
                GlobalData.inventory.Add(item.name.Replace("(Clone)", ""));
            }

            float power = clickedItem.GetComponent<Weapon>().power;
            GlobalData.equippedStaffItem = name;
            GlobalData.playerPower = power;
            player.GetComponent<Player>().power = power;

        }
        else if(clickedItem.GetComponent<Armour>() != null)
        {
            if(amourSlot.transform.childCount > 0)
            {
                GameObject item = amourSlot.transform.GetChild(0).gameObject;
                GlobalData.inventory.Add(item.name.Replace("(Clone)", ""));
            }

            float armour = clickedItem.GetComponent<Armour>().armour;
            GlobalData.equippedArmourItem = name;
            GlobalData.playerArmour = armour;
            player.GetComponent<Player>().armour = armour;
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
            player.GetComponent<Player>().power = 0;
            GlobalData.equippedStaffItem = null;
        }
        else if (clickedItem.GetComponent<Armour>() != null)
        {
            GlobalData.equippedArmourItem = null;
            player.GetComponent<Player>().armour = 0;
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

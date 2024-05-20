using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerDownHandler
{
    public static bool GameIsPaused = false;
    public GameObject inventoryUI;
    public GameObject staffSlot;
    public GameObject amourSlot;
    public GameObject InventorySlots;
    private GameObject clickedItem;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
                displayInventoryItems();
            }
        }
    }

    private void displayInventoryItems()
    {
        for(int i = 0; i < InventorySlots.transform.childCount; i++)
        {
            if(InventorySlots.transform.GetChild(i).childCount > 0)
            {
                Destroy(InventorySlots.transform.GetChild(i).GetChild(0).gameObject);
            }
        }

        int index = 0;
        foreach (string name in GlobalData.inventory)
        {
            Debug.Log(name);
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
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
        if(gameObject.CompareTag("Item"))
        {
            clickedItem = gameObject;
        }
    }

    public void equip()
    {
        if(!clickedItem)
        {
            return;
        }

        string name = clickedItem.name.Replace("(Clone)", "");
        if(clickedItem.GetComponent<Weapon>() != null)
        {
            if(staffSlot.transform.childCount > 0)
            {
                Transform nextSlot = InventorySlots.transform.GetChild(GlobalData.inventory.Count);
                staffSlot.transform.GetChild(0).gameObject.transform.SetParent(nextSlot);
            }
            GlobalData.equippedStaffItem = name;
        }


        GlobalData.inventory.Remove(name);
        displayInventoryItems();
        clickedItem = null;

    }

    public void unequip()
    {
        if (clickedItem.CompareTag("Item"))
        {
            Transform nextSlot = InventorySlots.transform.GetChild(GlobalData.inventory.Count);
            clickedItem.transform.SetParent(nextSlot);
            GlobalData.inventory.Add(clickedItem.name.Replace("(Clone)", ""));
            GlobalData.equippedStaffItem = null;
            displayInventoryItems();
        }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour, IPointerDownHandler
{
    public static bool GameIsPaused = false;
    public GameObject inventoryUI;
    public GameObject staffSlot;
    public GameObject amourSlot;
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
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
        Debug.Log(gameObject.CompareTag("Weapon Modifier"));
        if(gameObject.CompareTag("Weapon Modifier"))
        {
            clickedItem = gameObject;
        }
    }

    public void equip()
    {
        if(clickedItem.CompareTag("Weapon Modifier"))
        {
            clickedItem.transform.SetParent(staffSlot.transform);
            GlobalData.equippedStaffItem = clickedItem;
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

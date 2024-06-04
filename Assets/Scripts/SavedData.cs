using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedData
{
    public static float playerMaxMana;
    public static float playerMaxHealth;
    public static int playerXP;
    public static float cameraZoom = 5;
    public static string equippedStaffItem;
    public static string equippedArmourItem;
    public static float playerPower = 1;
    public static float playerArmour;
    public static int currentSceneIndex = 0;
    public static ArrayList inventory = new ArrayList();

    public SavedData()
    {
        playerMaxMana = GlobalData.playerMaxMana;
        playerMaxHealth = GlobalData.playerMaxHealth;
        playerXP = GlobalData.playerXP;
        cameraZoom = GlobalData.cameraZoom;
        equippedStaffItem = GlobalData.equippedStaffItem;
        equippedArmourItem = GlobalData.equippedArmourItem;
        playerPower = GlobalData.playerPower;
        playerArmour = GlobalData.playerArmour;
        currentSceneIndex = GlobalData.currentSceneIndex;
        inventory = GlobalData.inventory;
    }

    public void LoadData()
    {
        GlobalData.playerMaxMana = playerMaxMana;
        GlobalData.playerMaxHealth = playerMaxHealth;
        GlobalData.playerXP = playerXP;
        GlobalData.cameraZoom = cameraZoom;
        GlobalData.equippedStaffItem = equippedStaffItem;
        GlobalData.equippedArmourItem = equippedArmourItem;
        GlobalData.playerPower = playerPower;
        GlobalData.playerArmour = playerArmour;
        GlobalData.currentSceneIndex = currentSceneIndex;
        GlobalData.inventory = inventory;
    }
}

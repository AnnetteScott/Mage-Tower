using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{
    public static float playerMaxMana;
    public static float playerMaxHealth;
    public static int playerXP;
    public static int playerLevel;
    public static float cameraZoom = 5;
    public static string equippedStaffItem;
    public static string equippedArmourItem;
    public static float playerPower = 1;
    public static float playerArmour;
    public static int currentSceneIndex = 0;
    public static ArrayList inventory = new ArrayList();

    public static void reset()
    {
        playerMaxMana = 0f;
        playerMaxHealth = 0f;
        playerXP = 0;
        playerLevel = 1;
        equippedStaffItem = null;
        equippedArmourItem = null;
        playerPower = 1;
        playerArmour = 1;
        currentSceneIndex = 0;
        inventory = new ArrayList();
    }
}
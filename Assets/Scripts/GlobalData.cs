using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{
    public static float playerMaxMana;
    public static float playerMaxHealth;
    public static int playerXP;
    public static float cameraZoom = 5;
    public static string equippedStaffItem;
    public static string equippedArmourItem;
    public static float playerPower = 1;
    public static float playerArmour;
    public static ArrayList inventory = new ArrayList();
    public static bool[] puzzlesCompleted = new bool[5]; 

    public static bool level1PuzzleCompleted;
    public static bool level3PuzzleCompleted;
    public static bool level5PuzzleCompleted;
    public static bool level7PuzzleCompleted;
    public static bool level9PuzzleCompleted;
}

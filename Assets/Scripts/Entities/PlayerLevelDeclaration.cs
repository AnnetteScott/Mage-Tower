using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelDeclaration : MonoBehaviour
{
    public float PlayerLevel = 1;
    public float XP = 0;
    public float NeededXPUntilLevelUp = 100;

    void Update()
    {
        AddXP(20);
        LevelUp();
    }

    public void AddXP(float amount)
    {
        XP += amount;
    }

    private void LevelUp()
    {
        while (XP >= NeededXPUntilLevelUp)
        {
            XP -= NeededXPUntilLevelUp;
            PlayerLevel++;
            NeededXPUntilLevelUp = CalculateNeededXPForNextLevel();
            Debug.Log($"Level Up! New Level: {PlayerLevel}");
        }
    }

    private float CalculateNeededXPForNextLevel()
    {
        return NeededXPUntilLevelUp + (PlayerLevel * 3f);
    }
}

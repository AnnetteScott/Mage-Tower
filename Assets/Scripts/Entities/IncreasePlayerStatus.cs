using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float Health = 100;
    public float Mana = 100;

    private PlayerLevelDeclaration PT;

    void Start()
    {
        PT = GetComponent<PlayerLevelDeclaration>(); // Get PlayerLevelDeclaration from the same GameObject
    }

    void Update()
    {
        PT.AddXP(10); // Example: Simulate gaining 10 XP per frame (you can adjust this)

        if (PT.PlayerLevel > 1) // Check if the player's level is greater than 1 (to avoid initial level-up on start)
        {
            Health += PT.PlayerLevel / 2f; // Increase health based on player's level
            Mana += PT.PlayerLevel / 2f; // Increase magic based on player's level
        }
    }
}

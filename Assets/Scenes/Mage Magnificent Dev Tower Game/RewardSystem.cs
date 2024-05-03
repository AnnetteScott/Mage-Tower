using System;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem : MonoBehaviour
{
    public static RewardSystem Instance { get; private set; }

    public enum RewardType { Coins, InventoryItem, ContentUnlock }

    private Dictionary<string, int> playerRewards = new Dictionary<string, int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure RewardSystem persists across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Prevent multiple instances
        }
    }

    public void GiveReward(RewardType rewardType)
    {
        string playerId = "Player1"; // Placeholder for player ID
        int rewardAmount = 0;

        switch (rewardType)
        {
            case RewardType.Coins:
                rewardAmount = 100; // Example amount
                Debug.Log($"{playerId} received 100 coins.");
                break;
            case RewardType.InventoryItem:
                // Assuming you have a way to add inventory items
                Debug.Log($"{playerId} received a new inventory item.");
                break;
            case RewardType.ContentUnlock:
                // Assuming you have a way to unlock content
                Debug.Log($"{playerId} unlocked new content.");
                break;
            default:
                Debug.LogError($"Unknown reward type: {rewardType}");
                break;
        }

        if (!playerRewards.ContainsKey(playerId))
        {
            playerRewards[playerId] = 0;
        }
        playerRewards[playerId] += rewardAmount;
    }

    public void DisplayPlayerRewards(string playerId)
    {
        if (playerRewards.ContainsKey(playerId))
        {
            Debug.Log($"Player {playerId} has {playerRewards[playerId]} coins.");
        }
        else
        {
            Debug.Log($"No rewards found for player {playerId}.");
        }
    }
}

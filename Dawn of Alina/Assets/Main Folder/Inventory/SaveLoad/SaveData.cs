using Language.Lua;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SaveData // Manages the player's Save Data as a class
{
    public string[] itemNames; // Names of the items in the player inventory
    public int[] itemAmounts; // Amounts of items in player inventory
    public float playerHealth; // int for the health of the player at save
    public float[] playerPos; // xyz position of player at save
   
    public SaveData(string[] itemNames, int[] itemAmounts)
    {
        Debug.Log(InventoryManager.instance.ItemData.Count);
        this.itemNames = itemNames;
        this.itemAmounts = itemAmounts;
        PlayerHealth PlayerData = PlayerHealth.instance; // Get Playerhealth singleton
        playerHealth = PlayerData.health;
        playerPos = new float[3]; // store the player position as array
        playerPos[0] = PlayerData.Player.transform.position.x;
        playerPos[1] = PlayerData.Player.transform.position.y;
        playerPos[2] = PlayerData.Player.transform.position.z;

      
        
    }
}
